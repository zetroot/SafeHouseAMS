using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using SafeHouseAMS.Backend.Server.IdentityProvider.Exceptions;
using SafeHouseAMS.Backend.Server.IdentityProvider.Models;
using SafeHouseAMS.BizLayer.Users;
using SafeHouseAMS.BizLayer.Users.Commands;

namespace SafeHouseAMS.Backend.Server.IdentityProvider.Repositories
{
    internal class UserRepository : IUserRepository, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient(nameof(UserRepository));
        }

        public async Task CreateAsync(CreateUserCommand command)
        {
            string identityProviderUrlBase = _configuration.GetValue<string>("oidc:Authority");
            string accessToken = await RetrieveAccessToken(identityProviderUrlBase);
            await CreateUser(identityProviderUrlBase, accessToken, _mapper.Map<CreateUserRequestIdentityProviderModel>(command));
        }

        private async Task<string> RetrieveAccessToken(string urlBase)
        {
            string url = urlBase + "/connect/token";
            IEnumerable<KeyValuePair<string?,string?>> formToSend = new List<KeyValuePair<string?,string?>>
            {
                new KeyValuePair<string?, string?>("client_id", "api"),
                new KeyValuePair<string?, string?>("client_secret", "secret"),
                new KeyValuePair<string?, string?>("grant_type", "client_credentials"),
            };

            var httpResponseMessage =  await _httpClient.PostAsync(url, new FormUrlEncodedContent(formToSend));
            var responseText = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new FailedToRetrieveAccessTokenException(responseText);
            }

            RetrieveTokenResponse parsedResponse = JsonSerializer.Deserialize<RetrieveTokenResponse>(responseText)
                                                   ?? throw new FailedToRetrieveAccessTokenException("Failed to deserialize response");
            return parsedResponse.AccessToken ?? throw new FailedToRetrieveAccessTokenException("Parsed access token is null");
        }

        private async Task CreateUser(string urlBase, string accessToken, CreateUserRequestIdentityProviderModel request)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Content = JsonContent.Create(request);
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri(urlBase + "/users");
            var response = await _httpClient.SendAsync(httpRequestMessage);
            if (!response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                throw new FailedToCreateUserException(responseMessage);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}

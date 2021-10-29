using System.Threading.Tasks;
using Grpc.Net.Client;
using SafeHouseAMS.BizLayer.Users;
using SafeHouseAMS.BizLayer.Users.Commands;
using SafeHouseAMS.Transport.Protos.Models.Users;
using SafeHouseAMS.Transport.Protos.Services;

namespace SafeHouseAMS.WasmApp.Services
{
    internal class UserCatalogueClient : IUserCatalogue
    {
        private readonly UserCatalogue.UserCatalogueClient _client;

        public UserCatalogueClient(GrpcChannel channel)
        {
            _client = new UserCatalogue.UserCatalogueClient(channel);
        }

        public async Task CreateAsync(CreateUserCommand command)
        {
            await _client.CreateAsync(new CreateUserRequest { Email = command.Email, FirstName = command.FirstName, LastName = command.LastName });
        }
    }
}
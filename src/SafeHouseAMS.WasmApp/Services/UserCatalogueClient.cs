using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
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
        private readonly IMapper _mapper;
        public UserCatalogueClient(GrpcChannel channel, IMapper mapper)
        {
            _mapper = mapper;
            _client = new UserCatalogue.UserCatalogueClient(channel);
        }

        public async Task CreateAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            await _client.CreateAsync(_mapper.Map<CreateUserRequest>(command), new CallOptions(cancellationToken: cancellationToken));
        }
    }
}

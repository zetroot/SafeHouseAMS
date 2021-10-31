using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Users.Commands;

namespace SafeHouseAMS.BizLayer.Users
{
    internal class UserCatalogue : IUserCatalogue
    {
        private readonly IUserRepository _userRepository;

        public UserCatalogue(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            await command.ApplyOn(_userRepository);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Users.Commands;

namespace SafeHouseAMS.BizLayer.Users
{
    internal class UserCatalogue : IUserCatalogue
    {
        public Task CreateAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
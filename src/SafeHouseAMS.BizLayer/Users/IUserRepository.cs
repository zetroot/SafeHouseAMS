using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Users.Commands;

namespace SafeHouseAMS.BizLayer.Users
{
    /// <summary>
    /// Repository, which is responsible for user storage
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Create user in storage
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task Create(CreateUserCommand command);
    }
}
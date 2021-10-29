using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Users.Commands;

namespace SafeHouseAMS.BizLayer.Users
{
    /// <summary>
    /// Catalogue, which aggregates user-specific logic
    /// </summary>
    public interface IUserCatalogue
    {
        /// <summary>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task CreateAsync(CreateUserCommand command);
    }
}
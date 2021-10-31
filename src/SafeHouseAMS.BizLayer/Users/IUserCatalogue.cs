using System.Threading;
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
        /// <param name="command">Command to create a user</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task CreateAsync(CreateUserCommand command, CancellationToken cancellationToken);
    }
}
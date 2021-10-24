using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafeHouseAMS.IdentityProvider.Models;

namespace SafeHouseAMS.IdentityProvider.Controllers.API
{
    [Route("users")]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class UsersApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = $"{request.FirstName}_{request.LastName}"
            };
            // TODO: Hash passwords
            IdentityResult creationResult = await _userManager.CreateAsync(user, request.Password);
            if (creationResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(string.Join(", ", creationResult.Errors.Select(error => $"{error.Code}: {error.Description}")));
        }
    }
}
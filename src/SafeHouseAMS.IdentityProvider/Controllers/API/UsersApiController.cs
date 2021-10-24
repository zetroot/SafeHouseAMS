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
        public async Task<IActionResult> Register(RegisterUserRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            // TODO: Hash passwords
            await _userManager.CreateAsync(user, request.Password);
            return Ok();
        }
    }
}
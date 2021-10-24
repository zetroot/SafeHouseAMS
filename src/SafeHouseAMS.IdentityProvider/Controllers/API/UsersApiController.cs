using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IReadOnlyCollection<UserApiModel>> GetAll()
            => await _userManager.Users.Select(user => new UserApiModel(user.Id, user.FirstName, user.LastName, user.UserName)).ToArrayAsync();

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
            IdentityResult creationResult = await _userManager.CreateAsync(user, request.Password);
            if (creationResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(string.Join(", ", creationResult.Errors.Select(error => $"{error.Code}: {error.Description}")));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute]string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser userToDelete = await _userManager.FindByIdAsync(userId);
            if (userToDelete is null)
            {
                return BadRequest("User is not found");
            }

            IdentityResult creationResult = await _userManager.DeleteAsync(userToDelete);
            if (creationResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(string.Join(", ", creationResult.Errors.Select(error => $"{error.Code}: {error.Description}")));
        }
    }
}
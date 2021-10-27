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

            return BadRequest(SerializeErrors(creationResult));
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update([FromRoute] string userId, [FromBody] UpdateUserRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            try
            {
                await HandleUserProfileUpdateAsync(user, request);
                await HandlePasswordUpdateAsync(user, request);
            }
            catch (FailedUserUpdateException exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
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
                return NotFound();
            }

            IdentityResult creationResult = await _userManager.DeleteAsync(userToDelete);
            if (creationResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(SerializeErrors(creationResult));
        }

        private async Task HandleUserProfileUpdateAsync(ApplicationUser user, UpdateUserRequestModel request)
        {
            bool shouldUpdateProfile = false;
            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Email = request.Email;
                shouldUpdateProfile = true;
            }
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                user.FirstName = request.FirstName;
                shouldUpdateProfile = true;
            }
            if (!string.IsNullOrEmpty(request.LastName))
            {
                user.LastName = request.LastName;
                shouldUpdateProfile = true;
            }

            if (shouldUpdateProfile)
            {
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    throw new FailedUserUpdateException(SerializeErrors(updateResult));
                }
            }
        }

        private async Task HandlePasswordUpdateAsync(ApplicationUser user, UpdateUserRequestModel request)
        {
            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    throw new FailedUserUpdateException(SerializeErrors(changePasswordResult));
                }
            }
        }

        private static string SerializeErrors(IdentityResult creationResult)
            => string.Join(", ", creationResult.Errors.Select(error => $"{error.Code}: {error.Description}"));
    }
}
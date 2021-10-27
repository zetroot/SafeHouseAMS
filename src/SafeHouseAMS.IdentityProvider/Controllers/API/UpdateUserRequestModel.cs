using System.ComponentModel.DataAnnotations;

namespace SafeHouseAMS.IdentityProvider.Controllers.API
{
    public record UpdateUserRequestModel
    {
        [EmailAddress]
        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string CurrentPassword { get; init; }
        public string NewPassword { get; init; }
    }
}
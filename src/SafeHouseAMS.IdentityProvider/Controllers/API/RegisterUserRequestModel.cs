using System.ComponentModel.DataAnnotations;

namespace SafeHouseAMS.IdentityProvider.Controllers.API
{
    public record RegisterUserRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        [Required]
        public string FirstName { get; init; }
        [Required]
        public string LastName { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
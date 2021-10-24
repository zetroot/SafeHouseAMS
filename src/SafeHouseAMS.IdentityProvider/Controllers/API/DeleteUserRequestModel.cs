using System.ComponentModel.DataAnnotations;

namespace SafeHouseAMS.IdentityProvider.Controllers.API
{
    public record DeleteUserRequestModel
    {
        [Required]
        public string UserName { get; init; }
    }
}
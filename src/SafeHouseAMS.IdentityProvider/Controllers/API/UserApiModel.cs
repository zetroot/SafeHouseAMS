using System;

namespace SafeHouseAMS.IdentityProvider.Controllers.API
{
    public record UserApiModel(Guid Id, string FirstName, string LastName, string UserName);
}
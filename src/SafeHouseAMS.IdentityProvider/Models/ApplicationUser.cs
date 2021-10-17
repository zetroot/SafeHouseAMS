using System;
using Microsoft.AspNetCore.Identity;

namespace SafeHouseAMS.IdentityProvider.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
    }

    public class ApplicationRole : IdentityRole<Guid>
    {

    }
}

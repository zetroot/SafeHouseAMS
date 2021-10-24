using System;
using Microsoft.AspNetCore.Identity;

namespace SafeHouseAMS.IdentityProvider.Models
{
    /// <summary>
    /// Пользователь приложения
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

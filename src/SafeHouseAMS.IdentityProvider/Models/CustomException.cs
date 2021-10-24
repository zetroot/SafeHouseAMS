using System;

namespace SafeHouseAMS.IdentityProvider.Models
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}
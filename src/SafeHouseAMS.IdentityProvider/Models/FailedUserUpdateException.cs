using System;

namespace SafeHouseAMS.IdentityProvider.Models
{
    internal class FailedUserUpdateException : Exception
    {
        public FailedUserUpdateException(string message) : base(message)
        {
        }
    }
}
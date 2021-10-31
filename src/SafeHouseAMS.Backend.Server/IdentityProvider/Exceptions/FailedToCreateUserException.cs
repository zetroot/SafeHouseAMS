using System;

namespace SafeHouseAMS.Backend.Server.IdentityProvider.Exceptions
{
    internal class FailedToCreateUserException : Exception
    {
        public FailedToCreateUserException(string message) : base(message)
        {
        }
    }
}
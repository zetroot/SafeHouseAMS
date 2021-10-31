using System;

namespace SafeHouseAMS.Backend.Server.IdentityProvider.Exceptions
{
    internal class FailedToRetrieveAccessTokenException : Exception
    {
        public FailedToRetrieveAccessTokenException(string message) : base(message)
        {
        }
    }
}

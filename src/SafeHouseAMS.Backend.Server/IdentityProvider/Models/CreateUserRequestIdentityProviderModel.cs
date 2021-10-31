namespace SafeHouseAMS.Backend.Server.IdentityProvider.Models
{
    internal record CreateUserRequestIdentityProviderModel(string Email, string FirstName, string LastName, string Password);
}
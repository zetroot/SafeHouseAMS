using System.Text.Json.Serialization;

namespace SafeHouseAMS.Backend.Server.IdentityProvider.Models
{
    internal record RetrieveTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; init; }
    };
}
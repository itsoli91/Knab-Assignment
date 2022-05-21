using System.Text.Json.Serialization;

namespace Knab.Exchange.Api.Configs
{
    public class JwtSettings
    {
        [JsonPropertyName("Authority")]
        public string Authority { get; set; } = null!;
        [JsonPropertyName("ValidAudiences")]
        public bool ValidateAudience { get; set; }
        [JsonPropertyName("ValidAudiences")]
        public List<string> ValidAudiences { get; set; } = null!;

        [JsonPropertyName("ValidateIssuer")]
        public bool ValidateIssuer { get; set; }

        [JsonPropertyName("ValidIssuers")]
        public List<string> ValidIssuers { get; set; } = null!;

        [JsonPropertyName("RequireHttpsMetadata")]
        public bool RequireHttpsMetadata { get; set; }

        [JsonPropertyName("RequireSignedTokens")]
        public bool RequireSignedTokens { get; set; }

        [JsonPropertyName("ValidateIssuerSigningKey")]
        public bool ValidateIssuerSigningKey { get; set; }

    }
}
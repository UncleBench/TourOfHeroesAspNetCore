using System.ComponentModel.DataAnnotations;

namespace TourOfHeroes.Infrastructure.Authentication
{
    public sealed class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public const int SecretMinLength = 256;

        [MinLength(SecretMinLength)]
        public string Secret { get; init; } = null!;
        public int ExpiresInMinutes { get; init; }
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
    }
}

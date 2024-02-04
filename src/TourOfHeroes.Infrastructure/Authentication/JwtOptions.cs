namespace TourOfHeroes.Infrastructure.Authentication
{
    public sealed class JwtOptions
    {
        public const string SectionName = nameof(JwtOptions);
        public const int SecretMinLength = 256;

        public string Secret { get; init; } = null!;

        public int ExpiresInMinutes { get; init; }

        public string Issuer { get; init; } = null!;

        public string Audience { get; init; } = null!;
    }
}

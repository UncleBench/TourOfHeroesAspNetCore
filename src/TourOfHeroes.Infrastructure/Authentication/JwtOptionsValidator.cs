using FluentValidation;

namespace TourOfHeroes.Infrastructure.Authentication
{
    public sealed class JwtOptionsValidator : AbstractValidator<JwtOptions>
    {
        public JwtOptionsValidator()
        {
            RuleFor(x => x.Secret).MinimumLength(256);
            RuleFor(x => x.Issuer).NotEmpty();
            RuleFor(x => x.Audience).NotEmpty();
            RuleFor(x => x.ExpiresInMinutes).NotEmpty();
        }
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TourOfHeroes.Application.Authentication.Common;
using TourOfHeroes.Application.Common.Services;
using TourOfHeroes.Application.Common.Validation;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Application.Users.Persistence;
using TourOfHeroes.Infrastructure.Authentication;
using TourOfHeroes.Infrastructure.Common.Persistence;
using TourOfHeroes.Infrastructure.Heroes.Persistence;
using TourOfHeroes.Infrastructure.Services;
using TourOfHeroes.Infrastructure.Users.Persistence;

namespace TourOfHeroes.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddPersistence();
            services.AddAuth(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<TourOfHeroesDbContext>(options => options.UseSqlite("Data Source=TourOfHeroes.db"));
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfigurationManager configuration)
        {
            var jwtOptions = new JwtOptions();
            configuration.Bind(JwtOptions.SectionName, jwtOptions);

            services.AddSingleton(Options.Create(jwtOptions));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IValidator<JwtOptions>, JwtOptionsValidator>();
            services.AddSingleton(provider => provider.GetRequiredService<IOptions<JwtOptions>>().Value);

            var jtwOptions = services.AddOptionsWithValidateOnStart<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateFluently();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
                });

            return services;
        }
    }
}

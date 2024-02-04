using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence();
            services.AddJwt(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<TourOfHeroesDbContext>(options => options.UseSqlite("Data Source=TourOfHeroes.db"));
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IValidator<JwtOptions>, JwtOptionsValidator>();
            services.AddSingleton(provider => provider.GetRequiredService<IOptions<JwtOptions>>().Value);

            services.AddOptionsWithValidateOnStart<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateFluently()
                .ValidateOnStart();

            return services;
        }
    }
}

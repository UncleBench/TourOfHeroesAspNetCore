using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TourOfHeroes.Application.Common.Authentication;
using TourOfHeroes.Application.Common.Services;
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
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<TourOfHeroesDbContext>(options => options.UseSqlite("Data Source=TourOfHeroes.db"));
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}

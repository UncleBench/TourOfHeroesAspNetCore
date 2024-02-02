using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Infrastructure.Common.Persistence;
using TourOfHeroes.Infrastructure.Heroes.Persistence;

namespace TourOfHeroes.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<TourOfHeroesDbContext>(options => options.UseSqlite("Data Source=TourOfHeroes.db"));
            services.AddScoped<IHeroRepository, HeroRepository>();

            return services;
        }
    }
}

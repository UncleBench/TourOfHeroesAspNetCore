using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using TourOfHeroes.Application.Common.Behaviours;

namespace TourOfHeroes.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));

            return services;
        }
    }
}

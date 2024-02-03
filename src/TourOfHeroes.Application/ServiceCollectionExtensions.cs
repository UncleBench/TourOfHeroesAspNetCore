using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TourOfHeroes.Application.Common.Behaviours;
using TourOfHeroes.Application.Security.Authentication;

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

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}

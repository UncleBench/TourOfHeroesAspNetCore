using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;
using TourOfHeroes.Api.Common;

namespace TourOfHeroes.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddControllers();

            // Configure OpenAPI
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer()
                .AddSwaggerDocument(options =>
                {
                    options.PostProcess = document =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "Tour of Heroes API";
                        document.Info.Description = "REST API for Tour of Heroes";
                    };
                });

            // Configure allowed origins for CORS
            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
            if (allowedOrigins != null)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.WithOrigins(allowedOrigins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });
            }

            // Generate lowercase URLs and query strings
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            // Add custom ProblemDetailsFactory
            services.AddSingleton<ProblemDetailsFactory, TourOfHeroesProblemDetailsFactory>();

            // Add Mapster
            services.AppMappings();

            return services;
        }

        private static IServiceCollection AppMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddSingleton<IMapper, ServiceMapper>();

            return services;
        }
    }
}

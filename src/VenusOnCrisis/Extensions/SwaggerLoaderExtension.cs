using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace VenusOnCrisis.Extensions
{
    public static class SwaggerLoaderExtension
    {
        public static IServiceCollection AddSwaggerLoader(this IServiceCollection services, IConfiguration config)
        {
            // endpoint documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VenusOnCrisis", Version = "v1" });
            });

            return services;
        }
    }
}
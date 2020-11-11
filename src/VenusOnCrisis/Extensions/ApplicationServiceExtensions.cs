using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using VenusOnCrisis.Data;
using Microsoft.EntityFrameworkCore;
using VenusOnCrisis.Services;
using VenusOnCrisis.Interfaces;

namespace VenusOnCrisis.Extensions
{
    public static class ApplicationServiceExtensions
    {
        // db configuration
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddDbContext<DataContext>(options => 
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

        return services;
        }
    }
}
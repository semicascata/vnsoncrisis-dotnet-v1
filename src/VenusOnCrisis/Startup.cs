using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VenusOnCrisis.Extensions;

namespace VenusOnCrisis
{
    public class Startup
    {
        // private readonly _config: IConfiguration
        private readonly IConfiguration _config;

        // constructor(@Startup() private _config: config) {}
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplicationServices(_config); // like "loaders"
            services.AddControllers();
            services.AddCors(); // app.enableCors();
            services.AddIdentityServices(_config); // jwt-identity loader
            services.AddSwaggerLoader(_config); // swagger loader
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // swagger endpoint docs
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VenusOnCrisis v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(cb => 
                cb.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("https://localhost:4200"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

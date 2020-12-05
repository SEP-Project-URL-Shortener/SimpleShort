/*
 * Startup:
 * Responsible for declaring services and pipelines.
 * This is also used to dependency inject all repositories and services.
 */

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SimpleShort.Data;
using SimpleShort.Data.LogService;

namespace SimpleShort
{
    public class Startup
    {

        public readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShortenedUrlContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("ShortenedUrls"));
            });

            services.AddScoped<IShortenUrlRepository, ShortenUrlRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, LogService>();

            services.AddControllers();
            services.AddControllersWithViews();

            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc(Configuration["SwaggerUIDocs:Title"], new OpenApiInfo
                {
                    Title = Configuration["SwaggerUIDocs:Title"],
                    Version = Configuration["SwaggerUIDocs:Version"],
                    Description = Configuration["SwaggerUIDocs:Description"]
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                Console.ForegroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            app.UseSwagger();
            app.UseSwaggerUI(endpoints =>
            {
                endpoints.SwaggerEndpoint(
                    $"/swagger/{Configuration["SwaggerUIDocs:Title"]}/swagger.json",
                    Configuration["SwaggerUIDocs:Title"]
                    );
            });

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}

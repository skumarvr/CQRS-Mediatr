using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;
using TechChallenge.Data.Configuration;
using TechChallenge.Domain.Configuration;
using TechChallenge.Infrastructure.Configuration;

namespace TechChallenge.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void LogConnStr(string connStr)
        {
            var maskedConnStrArray = connStr.Split(';').Where(str => !str.Contains("password", StringComparison.OrdinalIgnoreCase)).ToArray();
            Log.Information($"Database Connection Str : {String.Join(';', maskedConnStrArray)}");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            string connStr = Configuration["ConnectionStr"];
            Log.Information("---------------------------------");
            LogConnStr(connStr);
            Log.Information($"Environment : {Configuration["ENVIRONMENT"]}");
            Log.Information("---------------------------------");

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // Adding Domain Services
            services.AddMediatR(typeof(DomainServicesCollectionExtensions).Assembly);
            services.AddDomainServices();

            // Adding Data Services
            services.AddDatabase(connStr);

            // Adding Data Services
            // services.AddDatabase(Configuration.GetConnectionString("HipagesDatabase"));

            // Adding Infrastructure Services
            services.AddRepository();
            services.AddEmailService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Disabled for using only port:80
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    Console.WriteLine("Starting AngularCliServer...");
                    spa.UseAngularCliServer(npmScript: "start");
                    // spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                }
            });
        }
    }
}

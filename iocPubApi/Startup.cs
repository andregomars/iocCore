using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

using iocPubApi.Models;
using iocPubApi.Repositories;

namespace iocPubApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                // .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<io_onlineContext>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("IO_OnlineDatabase")));
            services.AddMemoryCache();
            services.AddMvc();
            // services.AddLogging();
            services.AddCors();

            // Insert repositories
            services.AddSingleton<IFleetIdentityRepository, FleetIdentityRepository>();
            services.AddSingleton<IVehicleIdentityRepository, VehicleIdentityRepository>();
            services.AddSingleton<IVehicleStatusRepository, VehicleStatusRepository>();
            services.AddSingleton<IVehicleSnapshotRepository, VehicleSnapshotRepository>();
            services.AddSingleton<IVehicleSnapshotHistoryRepository, VehicleSnapshotHistoryRepository>();
            services.AddSingleton<IVehicleAlertRepository, VehicleAlertRepository>();
            services.AddSingleton<IVehicleDailyUsageRepository, VehicleDailyUsageRepository>();
            services.AddSingleton<IVehicleDailyFileRepository, VehicleDailyFileRepository>();
        
            // Add Swagger API documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "IO_Online API", Version = "V1" } );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            // loggerFactory.AddDebug();

            // app.UseCors("IocPubApiPolicy");
            // Must use before MVC, Set up CORS policy to allow *
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "IO_Online API V1");
            });
        }
    }
}

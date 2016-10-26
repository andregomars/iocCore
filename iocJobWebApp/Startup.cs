using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hangfire;
using iocCoreSMS.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
//using NLog.Extensions.Logging;

namespace iocJobWebApp
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = configBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            InitSMSManager();            

            var containerBuilder = new ContainerBuilder();
            //containerBuilder.RegisterType<SMSManager>().As<ISMSManager>();
            containerBuilder.RegisterInstance<ISMSManager>(SMSManager.Instance);
            containerBuilder.Populate(services);
            this.ApplicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
            }

            //add hangfire
            app.UseHangfireDashboard("");
            app.UseHangfireServer();

            //BackgroundJob.Enqueue<ISMSManager>( x => x.Send() );
            //RecurringJob.AddOrUpdate<ISMSManager>( x => x.Receive(), "*/5 * * * *");
            
            RecurringJob.AddOrUpdate<ISMSManager>( x => x.Send(), 
                Configuration["SMS.AttApi:SendSchedule"]);
            RecurringJob.AddOrUpdate<ISMSManager>( x => x.Receive(), 
                Configuration["SMS.AttApi:ReceiveSchedule"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Autofac: If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
        
        private void InitSMSManager()
        {
            SMSManager.Instance.UrlSendSMS = Configuration["SMS.AttApi:UrlSendSMS"];
            SMSManager.Instance.UrlReceiveSMS = Configuration["SMS.AttApi:UrlReceiveSMS"];
            SMSManager.Instance.UrlGetAccessToken = Configuration["SMS.AttApi:UrlGetAccessToken"];
            SMSManager.Instance.AppScope = Configuration["SMS.AttApi:AppScope"];
            SMSManager.Instance.AppKey = Configuration["SMS.AttApi:AppKey"];
            SMSManager.Instance.AppSecret = Configuration["SMS.AttApi:AppSecret"];
            SMSManager.Instance.VerifyMessageDeliveryStatus = 
                Convert.ToBoolean(Configuration["SMS.AttApi:VerifyMessageDeliveryStatus"]);
        }
    }
}

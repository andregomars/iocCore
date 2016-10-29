using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hangfire;
using iocCoreSMS.Models;
using iocCoreSMS.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;

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

            //initialize configuration, then inject as a singleton
            ISMSConfiguration config = InitSMSManager();            

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(config).SingleInstance().As<ISMSConfiguration>();
            containerBuilder.RegisterType<MessageBox>().As<IMessageBox>();
            containerBuilder.RegisterType<SMSManager>().As<ISMSManager>();
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
            
            RecurringJob.AddOrUpdate<ISMSManager>("Job-SendSMS", x => x.Send(), 
                Configuration["SMS.AttApi:SendSchedule"]);
            RecurringJob.AddOrUpdate<ISMSManager>("Job-ReceiveSMS", x => x.Receive(), 
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
        
        private ISMSConfiguration InitSMSManager()
        {
            var config = new SMSConfiguration();
            config.ShortCode = Configuration["SMS.AttApi:ShortCode"];
            config.UrlSendSMS = Configuration["SMS.AttApi:UrlSendSMS"]
                .Replace("{{shortcode}}", config.ShortCode);
            config.UrlReceiveSMS = Configuration["SMS.AttApi:UrlReceiveSMS"]
                .Replace("{{shortcode}}", config.ShortCode);
            config.UrlGetAccessToken = Configuration["SMS.AttApi:UrlGetAccessToken"];
            config.AppScope = Configuration["SMS.AttApi:AppScope"];
            config.AppKey = Configuration["SMS.AttApi:AppKey"];
            config.AppSecret = Configuration["SMS.AttApi:AppSecret"];
            config.VerifyMessageDeliveryStatus = 
                Convert.ToBoolean(Configuration["SMS.AttApi:VerifyMessageDeliveryStatus"]);
            config.BaseUrlMessageApi = Configuration["SMS.AttApi:BaseUrlMessageApi"];

            return config;
        }
    }
}

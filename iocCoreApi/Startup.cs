using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Hangfire;

[assembly: OwinStartup(typeof(iocCoreApi.Startup))]

namespace iocCoreApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("IO_OnlineDBConn");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}

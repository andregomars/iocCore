using Autofac;
using Microsoft.Extensions.Logging;
using iocCoreSMS.Services;

namespace iocJobWebApp
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method in ConfigureServices.

            //builder.Register(c => new ISMSManager(c.Resolve<ILogger<ValuesService>>()))
            //    .As<IValuesService>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<SMSManager>().As<ISMSManager>().InstancePerLifetimeScope();
            //builder.RegisterType<SMSManager>();
        }
    }
}

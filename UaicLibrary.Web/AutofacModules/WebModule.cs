using Autofac;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.AutofacModules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TokenGenerator>().As<ITokenGenerator>().InstancePerDependency();
        }
    }
}

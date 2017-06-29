using Autofac;
using UaicLibrary.DataBase.Contexts;

namespace UaicLibrary.Web.AutofacModules
{
    public class DataBaseModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UaicLibraryContext>().InstancePerDependency();
        }
    }
}

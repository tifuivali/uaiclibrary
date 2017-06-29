using System.Reflection;
using Autofac;
using UaicLibrary.Domain.UserManagement;
using Module = Autofac.Module;

namespace UaicLibrary.Web.AutofacModules
{
    public class RepositoriesModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(UserRepository).GetTypeInfo().Assembly)
                .Where(type => (type.Name.EndsWith("Repository")
                                || type.Name.EndsWith("Service")
                                || type.Name.EndsWith("Provider"))
                               && !type.IsConstructedGenericType)
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}

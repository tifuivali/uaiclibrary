using System.Reflection;
using Autofac;
using UaicLibrary.Common.Error;
using UaicLibrary.Domain.UserManagement;
using Module = Autofac.Module;

namespace UaicLibrary.Web.AutofacModules
{
    public class ValidatorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(UserRepository).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IModelValidator<>))
                .InstancePerDependency();
        }
    }
}

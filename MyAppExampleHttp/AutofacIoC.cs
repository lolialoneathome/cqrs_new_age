using Autofac;
using cqrs_review_windsor.Command;
using cqrs_review_windsor.Queries;
using MyAppExampleHttp.Command;
using MyAppExampleHttp.Queries;
using System.Reflection;

namespace MyAppExampleHttp
{
    public static class AutofacIoC
    {
        public static void AddCqrsHttpAutofac(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyTypes(typeof(GetUserByIdHttp)
                .GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IQuery<,>));
            containerBuilder.RegisterAssemblyTypes(typeof(AddUserHttp)
                .GetTypeInfo().Assembly).AsClosedTypesOf(typeof(ICommand<>));
        }
    }
}

using Autofac;
using cqrs_review_windsor.Command;
using cqrs_review_windsor.Queries;
using MyAppExampleSql.Command;
using MyAppExampleSql.Queries;
using System.Reflection;

namespace MyAppExampleSql
{
    public static class AutofacIoC
    {
        public static void AddCqrsSqlAutofac(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyTypes(typeof(GetUserByIdSql)
                .GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IQuery<,>));
            containerBuilder.RegisterAssemblyTypes(typeof(AddUserSql)
                .GetTypeInfo().Assembly).AsClosedTypesOf(typeof(ICommand<>));
        }
    }
}

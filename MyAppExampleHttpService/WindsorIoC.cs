using Castle.MicroKernel.Registration;
using Castle.Windsor;
using cqrs_review_windsor.Command;
using cqrs_review_windsor.Queries;
using MyAppExampleSql.Command;
using MyAppExampleSql.Queries;

namespace MyAppExampleSql
{
    public static class WindsorIoC
    {
        public static void AddCqrsSqlWindsor(this WindsorContainer container)
        {
            container.Register(AllTypes
                .FromAssembly(typeof(AddUserSql).Assembly)
                .BasedOn(typeof(ICommand<>)).LifestyleScoped());

            container.Register(AllTypes
                .FromAssembly(typeof(GetUserById).Assembly)
                .BasedOn(typeof(IQuery<,>)).LifestyleScoped());
        }
    }
}

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
                .BasedOn(typeof(ICommand<>))
                .WithService.FromInterface()
                .LifestyleTransient());

            container.Register(AllTypes
                .FromAssembly(typeof(GetUserByIdSql).Assembly)
                .BasedOn(typeof(IQuery<,>))
                .WithService.FromInterface()
                .LifestyleTransient());
        }
    }
}

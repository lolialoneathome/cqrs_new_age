using Castle.MicroKernel.Registration;
using Castle.Windsor;
using cqrs_review_windsor.Command;
using cqrs_review_windsor.Queries;
using MyAppExampleHttp.Command;
using MyAppExampleHttp.Queries;

namespace MyAppExampleHttp
{
    public static class WindsorIoC
    {
        public static void AddCqrsHttpWindsor(this WindsorContainer container)
        {
            container.Register(AllTypes
                .FromAssembly(typeof(AddUserHttp).Assembly)
                .BasedOn(typeof(ICommand<>))
                .WithService.FromInterface()
                .LifestyleTransient());

            container.Register(AllTypes
                .FromAssembly(typeof(GetUserByIdHttp).Assembly)
                .BasedOn(typeof(IQuery<,>))
                .WithService.FromInterface()
                .LifestyleTransient());
        }
    }
}

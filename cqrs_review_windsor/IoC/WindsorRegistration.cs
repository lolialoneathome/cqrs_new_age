using Castle;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using cqrs_review_windsor.Command;
using cqrs_review_windsor.Queries;

namespace cqrs_review_windsor.IoC
{
    public static class WindsorRegistration
    {
        public static void AddCqrsWindsor(this WindsorContainer container) {
            container.Register(Component
             .For(typeof(IQueryFor<>))
             .ImplementedBy(typeof(QueryFor<>)).LifestyleTransient());
            container.Register(Component
             .For(typeof(ICommandBuilder))
             .ImplementedBy(typeof(CommandBuilder)).LifestyleTransient());

            container.AddFacility<TypedFactoryFacility>();
            container.Register(
                Component.For<IQueryBuilder>()
                    .AsFactory()
            );
            container.Register(
                Component.For<IQueryFactory>()
                    .AsFactory()
            );
            container.Register(
                Component.For<ICommandFactory>()
                    .AsFactory()
            );
        }
    }
}

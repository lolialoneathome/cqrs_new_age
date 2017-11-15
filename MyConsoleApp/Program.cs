using Castle.MicroKernel.Registration;
using Castle.Windsor;
using cqrs_review_windsor.IoC;
using MyAppExampleHttp;
using MyAppExampleSql;
using System;
namespace MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            WindsorContainer windsorContainerSql = new WindsorContainer();
            windsorContainerSql.Register(Component
                .For(typeof(MyService))
                );

            // Проект только начался, пока что всё складываем в одну базу, соответственно работаем с сущностями через sql
            windsorContainerSql.AddCqrsWindsor();
            windsorContainerSql.AddCqrsSqlWindsor();
            var service = windsorContainerSql.Resolve<MyService>();
            service.CreateUser("nya", "nyanyan", 12).Wait();
            var users = service.GetUsersByAge().Result;

            //Запили половину проекта, внезапно, оказывается, что чё-то куда-то выносится в отдельный сервис 
            //и теперь с User и Roles надо работать например по http
            Console.WriteLine("А сейчас я делаю новый контейнер, который работает с http и выполню те же действия");
            //На практике это будет конечно не в одном проекте.

            WindsorContainer windsorContainerHttp = new WindsorContainer();
            windsorContainerHttp.Register(Component
                .For(typeof(MyService))
                );

            windsorContainerHttp.AddCqrsWindsor();
            windsorContainerHttp.AddCqrsHttpWindsor();
            var service2 = windsorContainerHttp.Resolve<MyService>();
            service2.CreateUser("nya", "nyanyan", 12).Wait();
            var users2 = service2.GetUsersByAge().Result;

            Console.ReadLine();
        }
    }
}

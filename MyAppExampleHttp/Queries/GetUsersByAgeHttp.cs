using cqrs_review_windsor.Queries;
using MyDomainCriteries;
using MyModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAppExampleHttp.Queries
{
    public class GetUsersByAgeHttp : IQuery<AgeCriteria, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> AskAsync(AgeCriteria criteria)
        {
            //Представляем, что сходили по http к сервису, достали данные
            Console.WriteLine("I HTTP. GET USER BY AGE");
            return await Task.FromResult(new List<User>() { new User() { Id = 12, Age = criteria.Age }, new User() { Id = 22, Age = criteria.Age } });
        }
    }
}

using cqrs_review_windsor.Queries;
using MyDomainCriteries;
using MyModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAppExampleSql.Queries
{
    public class GetUsersByAge : IQuery<AgeCriteria, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> AskAsync(AgeCriteria criteria)
        {
            Console.WriteLine("I SQL. GET USER BY AGE");
            return await Task.FromResult(new List<User>() { new User() { Id = 12, Age = criteria.Age }, new User() { Id = 22, Age = criteria.Age } });
        }
    }
}

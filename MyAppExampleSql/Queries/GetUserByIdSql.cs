using System.Threading.Tasks;
using cqrs_review_windsor.Queries;
using MyDomainCriteries;
using MyModel;
using System;

namespace MyAppExampleSql.Queries
{
    public class GetUserByIdSql : IQuery<IdCriteria, User>
    {
        public async Task<User> AskAsync(IdCriteria criteria)
        {
            Console.WriteLine("I SQL. GET USER BY ID");
            //Представляем, что сходили в базу, достали данные
            return await Task.FromResult(new User()
            {
                Id = criteria.Id
            });
        }
    }
}

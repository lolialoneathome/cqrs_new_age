using cqrs_review_windsor.Queries;
using MyDomainCriteries;
using MyModel;
using System;
using System.Threading.Tasks;

namespace MyAppExampleSql.Queries
{
    public class GetRoleByIdSql : IQuery<IdCriteria, Role>
    {
        public async Task<Role> AskAsync(IdCriteria criteria)
        {
            Console.WriteLine("I SQL. GET ROLE BY ID");
            //Представляем, что сходили в базу, достали данные
            return await Task.FromResult(new Role()
            {
                Id = criteria.Id
            });
        }
    }
}

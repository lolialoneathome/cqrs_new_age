using cqrs_review_windsor.Queries;
using MyDomainCriteries;
using MyModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAppExampleSql.Queries
{
    public class GetUsersByRoleSql : IQuery<RoleCriteria, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> AskAsync(RoleCriteria criteria)
        {
            Console.WriteLine("I SQL. GET USER BY ROLE");
            return await Task.FromResult(new List<User>() { new User() { Id = 12, Role= new Role() { Id = criteria.RoleId} } });
        }
    }
}

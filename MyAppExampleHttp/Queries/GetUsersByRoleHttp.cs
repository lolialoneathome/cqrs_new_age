using cqrs_review_windsor.Queries;
using MyDomainCriteries;
using MyModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAppExampleHttp.Queries
{
    public class GetUsersByRoleHttp : IQuery<RoleCriteria, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> AskAsync(RoleCriteria criteria)
        {
            //Представляем, что сходили по http к сервису, достали данные
            Console.WriteLine("I HTTP. GET USER BY ROLE");
            return await Task.FromResult(new List<User>() { new User() { Id = 12, Role= new Role() { Id = criteria.RoleId} } });
        }
    }
}

using cqrs_review_windsor.Queries;
using MyDomainCriteries;
using MyModel;
using System;
using System.Threading.Tasks;

namespace MyAppExampleHttp.Queries
{
    public class GetRoleByIdHttp : IQuery<IdCriteria, Role>
    {
        public async Task<Role> AskAsync(IdCriteria criteria)
        {
            Console.WriteLine("I HTTP. GET ROLE BY ID");
            //Представляем, что сходили по http к сервису, достали данные
            return await Task.FromResult(new Role()
            {
                Id = criteria.Id
            });
        }
    }
}

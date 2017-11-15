using System.Threading.Tasks;
using cqrs_review_windsor.Queries;
using MyDomainCriteries;
using MyModel;
using System;

namespace MyAppExampleHttp.Queries
{
    public class GetUserByIdHttp : IQuery<IdCriteria, User>
    {
        public async Task<User> AskAsync(IdCriteria criteria)
        {
            Console.WriteLine("I HTTP. GET USER BY ID");
            //Представляем, что сходили по http к сервису, достали данные
            return await Task.FromResult(new User()
            {
                Id = criteria.Id
            });
        }
    }
}

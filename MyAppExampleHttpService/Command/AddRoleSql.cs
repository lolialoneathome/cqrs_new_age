using cqrs_review_windsor.Command;
using MyDomainCommandContext;
using System;
using System.Threading.Tasks;

namespace MyAppExampleSql.Command
{
    public class AddRoleSql : ICommand<AddRoleCommandContext>
    {
        public async Task ExecuteAsync(AddRoleCommandContext commandContext)
        {

            Console.WriteLine("I SQL. ADD ROLE");
            //А тут надо проявить фантазию, и представить например что даппером хранимка дергается, которая возвращает id
            var rnd = await Task.FromResult(new Random());
            commandContext.Id = rnd.Next(0, 100);
        }
    }
}

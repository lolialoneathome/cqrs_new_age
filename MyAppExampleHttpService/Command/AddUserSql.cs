using System.Threading.Tasks;
using cqrs_review_windsor.Command;
using MyDomainCommandContext;
using System;

namespace MyAppExampleSql.Command
{
    public class AddUserSql : ICommand<AddUserCommandContext>
    {
        
        public async Task ExecuteAsync(AddUserCommandContext commandContext)
        {
            Console.WriteLine("I SQL. ADD USER");
            //А тут надо проявить фантазию, и представить например что даппером хранимка дергается, которая возвращает id
            var rnd = await Task.FromResult(new Random());
            commandContext.Id = rnd.Next(0, 100);
        }
    }
}

using System.Threading.Tasks;
using cqrs_review_windsor.Command;
using MyDomainCommandContext;
using System;

namespace MyAppExampleHttp.Command
{
    public class AddUserHttp : ICommand<AddUserCommandContext>
    {
        
        public async Task ExecuteAsync(AddUserCommandContext commandContext)
        {
            Console.WriteLine("I HTTP. ADD USER");
            //А тут надо проявить фантазию, и представить например что мы идем к другому сервису по http
            var rnd = await Task.FromResult(new Random());
            commandContext.Id = rnd.Next(0, 100);
        }
    }
}

using cqrs_review_windsor.Command;
using MyDomainCommandContext;
using System;
using System.Threading.Tasks;

namespace MyAppExampleHttp.Command
{
    public class AddRoleHttp : ICommand<AddRoleCommandContext>
    {
        public async Task ExecuteAsync(AddRoleCommandContext commandContext)
        {

            Console.WriteLine("I HTTP. ADD ROLE");
            //А тут надо проявить фантазию, и представить например что мы идем к другому сервису по http
            var rnd = await Task.FromResult(new Random());
            commandContext.Id = rnd.Next(0, 100);
        }
    }
}

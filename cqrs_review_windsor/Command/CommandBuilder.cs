using System.Threading.Tasks;

namespace cqrs_review_windsor.Command
{
    public class CommandBuilder : ICommandBuilder
    {
        private readonly ICommandFactory _factory;



        public CommandBuilder(ICommandFactory factory)
        {
            _factory = factory;
        }

        public async Task ExecuteAsync<TCommandContext>(TCommandContext commandContext)
            where TCommandContext : ICommandContext
        {
            await _factory.Create<TCommandContext>().ExecuteAsync(commandContext);
        }
    }
}

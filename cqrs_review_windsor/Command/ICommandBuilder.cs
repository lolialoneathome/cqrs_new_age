using System.Threading.Tasks;

namespace cqrs_review_windsor.Command
{
    public interface ICommandBuilder
    {
        Task ExecuteAsync<TCommandContext>(TCommandContext commandContext)
            where TCommandContext : ICommandContext;
    }
}

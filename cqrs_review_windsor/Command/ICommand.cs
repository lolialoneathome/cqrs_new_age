
using System.Threading.Tasks;

namespace cqrs_review_windsor.Command
{
    public interface ICommand<in TCommandContext>
            where TCommandContext : ICommandContext
    {
        Task ExecuteAsync(TCommandContext commandContext);
    }
}

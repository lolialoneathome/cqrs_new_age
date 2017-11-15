using System.Threading.Tasks;

namespace cqrs_review_windsor.Queries
{
    public interface IQuery<TCriteria, TResult> 
        where TCriteria : ICriteria
    {
        Task<TResult> AskAsync(TCriteria criteria);
    }
}
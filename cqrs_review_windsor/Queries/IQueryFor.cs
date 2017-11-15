using System.Threading.Tasks;

namespace cqrs_review_windsor.Queries
{
    public interface IQueryFor<TResult>
    {
        Task<TResult> With<TCriteria>(TCriteria criterion)
            where TCriteria : ICriteria;
    }
}

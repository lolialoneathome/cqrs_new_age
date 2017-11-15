using System.Threading.Tasks;

namespace cqrs_review_windsor.Queries
{
    public class QueryFor<TResult> : IQueryFor<TResult>
    {
        private readonly IQueryFactory _factory;


        public QueryFor(IQueryFactory factory)
        {
            _factory = factory;
        }

        public Task<TResult> With<TCriteria>(TCriteria criterion)
            where TCriteria : ICriteria
        {
            return _factory.Create<TCriteria, TResult>().AskAsync(criterion);
        }
    }
}
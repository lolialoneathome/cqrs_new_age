namespace cqrs_review_windsor.Queries
{
    public interface IQueryFactory
    {
        IQuery<TCriteria, TResult> Create<TCriteria, TResult>()
            where TCriteria : ICriteria;
    }
}

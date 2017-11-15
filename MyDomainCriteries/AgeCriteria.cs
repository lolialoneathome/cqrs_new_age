using cqrs_review_windsor.Queries;

namespace MyDomainCriteries
{
    public class AgeCriteria : ICriteria
    {
        public int Age { get; set; }
    }
}

using cqrs_review_windsor.Queries;

namespace MyDomainCriteries
{
    public class RoleCriteria : ICriteria
    {
        public int RoleId { get; set; } //Если надо или просто хочется, сюда можно хоть весь объект засовывать, но лучше только то, что необходимо.
    }
}

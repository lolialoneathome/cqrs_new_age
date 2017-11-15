using cqrs_review_windsor.Command;

namespace MyDomainCommandContext
{
    public class AddRoleCommandContext : ICommandContext
    {
        public string Title { get; set; }
        public int Id { get; set; } //! This field we will set after command exec
    }
}

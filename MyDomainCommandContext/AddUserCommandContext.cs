using cqrs_review_windsor.Command;

namespace MyDomainCommandContext
{
    public class AddUserCommandContext : ICommandContext
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Id { get; set; } //! This field we will set after command exec
    }
}

using cqrs_review_windsor.Command;
using cqrs_review_windsor.Queries;
using MyDomainCommandContext;
using MyDomainCriteries;
using MyModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp
{
    //Or Controller.
    public class MyService
    {
        protected readonly IQueryBuilder _queryBuilder;
        protected readonly ICommandBuilder _commandBuilder;
        public MyService(IQueryBuilder queryBuilder, ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
            _commandBuilder = commandBuilder ?? throw new ArgumentNullException(nameof(commandBuilder));
        }

        public async Task<bool> CreateUser(string name, string surname, int age)
        {
            var context = new AddUserCommandContext()
            {
                Name = name,
                Surname = surname,
                Age = age
            };
            await _commandBuilder.ExecuteAsync(context);

            return await Task.FromResult(context.Id == 0);
        }

        public async Task<IEnumerable<User>> GetUsersByAge()
        {
            return await _queryBuilder.For<IEnumerable<User>>().With(new AgeCriteria()
            {
                Age = 12
            });
        }
    }
}

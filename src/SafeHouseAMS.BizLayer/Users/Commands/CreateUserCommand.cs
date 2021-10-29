using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Users.Commands
{
    /// <summary>
    /// Command to create new user in system
    /// </summary>
    public class CreateUserCommand : BaseCommand<IUserRepository>
    {
        private const string DefaultUserPassword = "321312312aasds@#@#FAAX";

        /// <summary></summary>
        public string Email { get; }

        /// <summary></summary>
        public string FirstName { get; }

        /// <summary></summary>
        public string LastName { get; }

        /// <summary></summary>
        public string Password { get; }

        /// <param name="entityID"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public CreateUserCommand(Guid entityID, string email, string firstName, string lastName) : base(entityID)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = DefaultUserPassword;
        }

        internal override async Task ApplyOn(IUserRepository repository)
        {
            await repository.Create(this);
        }
    }
}
using CustomerManagement.API.Command.Interfaces;
namespace CustomerManagement.API.Command.Commands
{
    public class OpenNewAccountForUserCommand : ICommand
    {
        public OpenNewAccountForUserCommand() 
        {
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; }

        public Guid CustomerUid { get; set; }

        public long InitialCredit { get; set; }                
    }
}

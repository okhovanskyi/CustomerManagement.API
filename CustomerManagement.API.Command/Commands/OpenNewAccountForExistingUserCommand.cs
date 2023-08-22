using CustomerManagement.API.Command.Interfaces;
namespace CustomerManagement.API.Command.Commands
{
    public class OpenNewAccountForExistingUserCommand : ICommand
    {
        public OpenNewAccountForExistingUserCommand() 
        {
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }

        public Guid CustomerUid { get; set; }

        public long InitialCredit { get; set; }                
    }
}

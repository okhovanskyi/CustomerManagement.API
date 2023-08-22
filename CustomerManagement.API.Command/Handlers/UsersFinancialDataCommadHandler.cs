using CustomerManagement.API.Command.Commands;
using CustomerManagement.API.Command.Interfaces;
using CustomerManagement.API.Service.DataTransferObjects;
using CustomerManagement.API.Service.Interfaces;
using System.Net;


namespace CustomerManagement.API.Command.Handlers
{
    public class UsersFinancialDataCommadHandler : ICommandHandler<OpenNewAccountForExistingUserCommand, CommandResult>
    {
        private const string UserNotFoundErrorMessage = "User Not Found.";
        private const string AccountWasNotCreatedErrorMessage = "Account Was Not Created.";

        private const string CommandCompletedSuccesfullyMessage = "Command Completed Succesfully.";

        private readonly IFinancialService _financialService;
        private readonly IUserService _userService;

        public UsersFinancialDataCommadHandler(IFinancialService financialService, IUserService userService) 
        {
            _financialService = financialService;
            _userService = userService;
        }

        public async Task<CommandResult> HandleAsync(OpenNewAccountForExistingUserCommand command)
        {
            try
            {
                var userDto = await _userService.GetUserAsync(command.CustomerUid);

                if (userDto == null)
                {
                    return new CommandResult
                    {
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Message = UserNotFoundErrorMessage
                    };
                }

                var userAccount = await _financialService.CreateUserAccountAsync(userDto.UserId);

                if (userAccount == null)
                {
                    return new CommandResult
                    {
                        HttpStatusCode = HttpStatusCode.InternalServerError,
                        Message = AccountWasNotCreatedErrorMessage
                    };
                }

                if (command.InitialCredit != 0)
                {
                    await _financialService.CreateTransactionAsync(new TransactionDto
                    {
                        AccountNumber = userAccount.AccountNumber,
                        Amount = command.InitialCredit,
                        TransactionType = command.InitialCredit > 0 ?
                        Repository.Models.Enums.TransactionType.Debit :
                        Repository.Models.Enums.TransactionType.Credit
                    });
                }
            }
            catch (ArgumentException argumentException) 
            {
                return new CommandResult 
                { 
                    HttpStatusCode = HttpStatusCode.BadRequest, 
                    Message = argumentException.Message, 
                    AggregateException = new AggregateException(argumentException) };
            }

            return new CommandResult 
            { 
                HttpStatusCode = HttpStatusCode.OK,
                Message = CommandCompletedSuccesfullyMessage
            };
        }
    }
}

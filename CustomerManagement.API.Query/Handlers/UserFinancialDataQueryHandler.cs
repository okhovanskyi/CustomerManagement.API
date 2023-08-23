using CustomerManagement.API.Query.Interfaces;
using CustomerManagement.API.Query.Queries;
using CustomerManagement.API.Query.QueryResults;
using CustomerManagement.API.Service.Interfaces;
using System.Net;

namespace CustomerManagement.API.Query.Handlers
{
    public class UserFinancialDataQueryHandler : IQueryHandler<GetUserFinancialDataQuery, GetUserFinancialDataQueryResult>
    {
        private const string UserNotFoundErrorMessage = "User Not Found.";

        private const string QueryCompletedSuccesfullyMessage = "Query Completed Succesfully.";

        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;

        public UserFinancialDataQueryHandler(IAccountService accountService, ITransactionService transactionService, IUserService userService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _userService = userService;
        }

        public async Task<GetUserFinancialDataQueryResult> HandleAsync(GetUserFinancialDataQuery query)
        {
            try
            {
                var userDto = await _userService.GetUserAsync(query.CustomerUid);

                if (userDto == null)
                {
                    return new GetUserFinancialDataQueryResult
                    {
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Message = UserNotFoundErrorMessage
                    };
                }

                return new GetUserFinancialDataQueryResult
                {
                    UserDto = userDto,
                    TransactionDtos = await _transactionService.GetTransactionsAsync(userDto.UserId),
                    AccountBalanceDtos = await _accountService.GetAccountsBalanceAsync(userDto.UserId),
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = QueryCompletedSuccesfullyMessage
                };
            }
            catch (ArgumentException argumentException)
            {
                return new GetUserFinancialDataQueryResult
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = argumentException.Message,
                    AggregateException = new AggregateException(argumentException)
                };
            }
        }
    }
}

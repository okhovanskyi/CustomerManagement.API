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

                var result = new GetUserFinancialDataQueryResult
                {
                    UserDto = userDto,
                    AccountBalanceDtos = new List<Service.DataTransferObjects.AccountBalanceDto?>(),
                    TransactionDtos = new List<Service.DataTransferObjects.TransactionDto?>(),
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = QueryCompletedSuccesfullyMessage
                };

                var accounts = await _accountService.GetAccountsAsync(userDto.UserId);

                if (accounts == null || accounts.Count == 0)
                {
                    return result;
                }

                foreach (var account in accounts)
                {
                    if (account == null)
                    {
                        continue;
                    }

                    var transactions = await _transactionService.GetTransactionsAsync(account.AccountNumber);

                    if (transactions == null)
                    {
                        continue;
                    }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    var filterredTransactions = transactions.Where(transaction => transaction != null);

                    result.TransactionDtos.AddRange(filterredTransactions);

                    account.Balance = filterredTransactions.Sum(transaction =>
                    transaction.TransactionType == Persistence.Enums.TransactionType.Debit ?
                    transaction.Amount :
                    -transaction.Amount);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                    result.AccountBalanceDtos.Add(account);

                    userDto.Balance += account.Balance;
                }                

                return result;
            }
            catch (ArgumentException argumentException)
            {
                return new GetUserFinancialDataQueryResult
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = argumentException.Message                    
                };
            }
        }
    }
}

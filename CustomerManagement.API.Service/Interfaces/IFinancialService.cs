using CustomerManagement.API.Repository.Models;
using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Interfaces
{
    public interface IFinancialService
    {
        Task<UserAccountDto?> CreateUserAccountAsync(long userId);

        Task CreateTransactionAsync(TransactionDto transactionDto);

        Task<UserAccountBalance> GetUserAccountsBalanceAsync(long userId);
    }
}

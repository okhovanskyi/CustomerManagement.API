using CustomerManagement.API.Repository.Models;

namespace CustomerManagement.API.Repository.Interfaces
{
    public interface IFinancialRepository
    {
        Task<UserAccountBalance> CreateUserAccountAsync(long userId);

        Task CreateTransactionAsync(Transaction? transaction);

        Task<List<UserAccountBalance>> GetAccountsBalanceAsync(long userId);
        Task<List<Transaction>> GetTransactionsAsync(long userId);
    }
}

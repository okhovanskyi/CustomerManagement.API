using CustomerManagement.API.Repository.Models;

namespace CustomerManagement.API.Repository.Interfaces
{
    public interface IFinancialRepository
    {
        Task CreateUserAccountBalanceAsync(UserAccountBalance userAccountBalance);

        Task CreateTransactionAsync(Transaction transaction);

        Task<UserAccountBalance> GetUserAccountsBalanceAsync(long userId);

        Task<UserAccountBalance> UpdateUserAccountBalanceAsync(UserAccountBalance userAccountBalance);
    }
}

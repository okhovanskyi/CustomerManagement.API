using CustomerManagement.API.Repository.Models;

namespace CustomerManagement.API.Service.Interfaces
{
    public interface IFinancialService
    {
        Task CreateUserAccountBalanceAsync(UserAccountBalance userAccountBalance);

        Task CreateTransactionAsync(Transaction transaction);

        Task<UserAccountBalance> GetUserAccountsBalanceAsync(long userId);

        Task<UserAccountBalance> UpdateUserAccountBalanceAsync(UserAccountBalance userAccountBalance);
    }
}

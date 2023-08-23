using CustomerManagement.API.Repository.Models;

namespace CustomerManagement.API.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> CreateUserAccountAsync(long userId);

        Task<List<Account>> GetAccountsAsync(long userId);
        
    }
}

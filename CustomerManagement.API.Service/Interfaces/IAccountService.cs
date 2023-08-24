using CustomerManagement.API.Repository.Models;
using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Interfaces
{
    public interface IAccountService
    {
        Task<UserAccountDto?> CreateUserAccountAsync(long userId);

        Task<List<AccountBalanceDto?>> GetAccountsAsync(long userId);
    }
}

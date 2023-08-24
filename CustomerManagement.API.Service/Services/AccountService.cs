using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Service.DataTransferObjects;
using CustomerManagement.API.Service.Interfaces;
using CustomerManagement.API.Service.Mappers;

namespace CustomerManagement.API.Service.Services
{
    public class AccountService : IAccountService
    {
        private const string UserIdErrorMessage = "UserId value must be more than zero.";
        
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository) 
        {
            _accountRepository = accountRepository;
        }

        public async Task<UserAccountDto?> CreateUserAccountAsync(long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException(UserIdErrorMessage);
            }

            var userAccountBalance = await _accountRepository.CreateUserAccountAsync(userId);

            return userAccountBalance?.ToUserAccountDto();
        }

        public async Task<List<AccountBalanceDto?>> GetAccountsAsync(long userId)
        {
            if (userId <= 0)
            { 
                throw new ArgumentException(UserIdErrorMessage); 
            }

            var accountList = await _accountRepository.GetAccountsAsync(userId);

            if (accountList == null || accountList.Count == 0)
            {
                return new List<AccountBalanceDto?>();
            }

            return accountList
                .Where(account => account != null)
                .Select(account => account.ToAccountBalanceDto())
                .ToList();
        }
    }
}

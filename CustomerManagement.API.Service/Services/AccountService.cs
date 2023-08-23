using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Service.DataTransferObjects;
using CustomerManagement.API.Service.Interfaces;
using CustomerManagement.API.Service.Mappers;

namespace CustomerManagement.API.Service.Services
{
    public class AccountService : IAccountService
    {
        private const string UserIdErrorMessage = "UserId value must be more than zero.";
        
        private readonly IFinancialRepository _financialRepository;

        public AccountService(IFinancialRepository financialRepository) 
        {
            _financialRepository = financialRepository;
        }

        public async Task<UserAccountDto?> CreateUserAccountAsync(long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException(UserIdErrorMessage);
            }

            var userAccountBalance = await _financialRepository.CreateUserAccountAsync(userId);

            return userAccountBalance.ToUserAccountDto();
        }

        public async Task<List<AccountBalanceDto?>> GetAccountsBalanceAsync(long userId)
        {
            if (userId <= 0)
            { 
                throw new ArgumentException(UserIdErrorMessage); 
            }

            var accountBalanceList = await _financialRepository.GetAccountsBalanceAsync(userId);

            if (accountBalanceList == null || accountBalanceList.Count == 0)
            {
                return new List<AccountBalanceDto?>();
            }

            return accountBalanceList
                .Where(accountBalance => accountBalance != null)
                .Select(accountBalance => accountBalance.ToAccountBalanceDto())
                .ToList();
        }
    }
}

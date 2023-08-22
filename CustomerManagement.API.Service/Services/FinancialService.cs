using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Repository.Models;
using CustomerManagement.API.Service.Interfaces;

namespace CustomerManagement.API.Service.Services
{
    public class FinancialService : IFinancialService
    {
        private const string TransactionAmountErrorMessage = "Amount of a transaction must be more than zero.";
        private const string UserIdErrorMessage = "UserId value must be more than zero.";
        private const string BalanceErrorMessage = "Ballance must not be negative.";

        private readonly IFinancialRepository _financialRepository;

        public FinancialService(IFinancialRepository financialRepository) 
        {
            _financialRepository = financialRepository;
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction.Amount <= 0)
            {
                throw new ArgumentException(TransactionAmountErrorMessage);
            }

            await _financialRepository.CreateTransactionAsync(transaction);
        }

        public async Task CreateUserAccountBalanceAsync(UserAccountBalance userAccountBalance)
        {
            if (userAccountBalance == null)
            {
                throw new ArgumentNullException(nameof(userAccountBalance));
            }

            await _financialRepository.CreateUserAccountBalanceAsync(userAccountBalance);
        }

        public async Task<UserAccountBalance> GetUserAccountsBalanceAsync(long userId)
        {
            if (userId <= 0)
            { 
                throw new ArgumentException(UserIdErrorMessage); 
            }

            return await _financialRepository.GetUserAccountsBalanceAsync(userId);
        }
        
        public async Task<UserAccountBalance> UpdateUserAccountBalanceAsync(UserAccountBalance userAccountBalance)
        {
            if (userAccountBalance.Balance < 0)
            {
                throw new ArgumentException(BalanceErrorMessage);
            }

            return await _financialRepository.UpdateUserAccountBalanceAsync(userAccountBalance);
        }
    }
}

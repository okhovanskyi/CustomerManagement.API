using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Repository.Models;
using CustomerManagement.API.Service.DataTransferObjects;
using CustomerManagement.API.Service.Interfaces;
using CustomerManagement.API.Service.Mappers;

namespace CustomerManagement.API.Service.Services
{
    public class FinancialService : IFinancialService
    {
        private const string TransactionAmountErrorMessage = "Amount of a transaction not be zero.";
        private const string UserIdErrorMessage = "UserId value must be more than zero.";
        
        private readonly IFinancialRepository _financialRepository;

        public FinancialService(IFinancialRepository financialRepository) 
        {
            _financialRepository = financialRepository;
        }

        public async Task CreateTransactionAsync(TransactionDto transactionDto)
        {
            if (transactionDto == null)
            {
                throw new ArgumentNullException(nameof(transactionDto));
            }

            if (transactionDto.Amount == 0)
            {
                throw new ArgumentException(TransactionAmountErrorMessage);
            }

            await _financialRepository.CreateTransactionAsync(transactionDto.FromTransactionDto());
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

        public async Task<UserAccountBalance> GetUserAccountsBalanceAsync(long userId)
        {
            if (userId <= 0)
            { 
                throw new ArgumentException(UserIdErrorMessage); 
            }

            return await _financialRepository.GetUserAccountsBalanceAsync(userId);
        }
    }
}

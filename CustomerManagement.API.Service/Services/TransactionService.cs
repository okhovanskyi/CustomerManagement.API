using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Service.DataTransferObjects;
using CustomerManagement.API.Service.Interfaces;
using CustomerManagement.API.Service.Mappers;

namespace CustomerManagement.API.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private const string TransactionAmountErrorMessage = "Amount of a transaction must not be zero.";
        private const string UserIdErrorMessage = "UserId value must be more than zero.";

        private readonly IFinancialRepository _financialRepository;

        public TransactionService(IFinancialRepository financialRepository)
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

        public async Task<List<TransactionDto?>> GetTransactionsAsync(long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException(UserIdErrorMessage);
            }

            var transactionsList = await _financialRepository.GetTransactionsAsync(userId);

            return transactionsList
                .Where(transaction => transaction != null)
                .Select(transaction => transaction.ToTransactionDto())
                .ToList();
        }
    }
}

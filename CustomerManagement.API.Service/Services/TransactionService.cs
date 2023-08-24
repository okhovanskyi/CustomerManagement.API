using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Service.DataTransferObjects;
using CustomerManagement.API.Service.Interfaces;
using CustomerManagement.API.Service.Mappers;

namespace CustomerManagement.API.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private const string TransactionAmountErrorMessage = "Amount of a transaction must not be zero.";        

        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
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

            await _transactionRepository.CreateTransactionAsync(transactionDto.FromTransactionDto());
        }

        public async Task<List<TransactionDto?>> GetTransactionsAsync(Guid accountNumber)
        {
            var transactionsList = await _transactionRepository.GetTransactionsAsync(accountNumber);

            return transactionsList
                .Where(transaction => transaction != null)
                .Select(transaction => transaction.ToTransactionDto())
                .ToList();
        }
    }
}

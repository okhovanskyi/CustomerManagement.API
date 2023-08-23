using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Interfaces
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(TransactionDto transactionDto);

        Task<List<TransactionDto?>> GetTransactionsAsync(long userId);
    }
}

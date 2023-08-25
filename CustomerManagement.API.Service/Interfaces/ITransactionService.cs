using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> CreateTransactionAsync(TransactionDto transactionDto);

        Task<List<TransactionDto?>> GetTransactionsAsync(Guid accountNumber);
    }
}

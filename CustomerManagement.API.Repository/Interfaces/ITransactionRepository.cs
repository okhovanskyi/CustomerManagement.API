using CustomerManagement.API.Repository.Models;

namespace CustomerManagement.API.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> CreateTransactionAsync(Transaction? transaction);

        Task<List<Transaction>> GetTransactionsAsync(Guid accountNumber);
    }
}

using CustomerManagement.API.Persistence;
using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Repository.Models;

namespace CustomerManagement.API.Repository.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionCollection _transactionCollection;

        public TransactionRepository(TransactionCollection transactionCollection) 
        {
            _transactionCollection = transactionCollection;
        }

        public async Task<bool> CreateTransactionAsync(Transaction? transaction)
        {
            if (transaction == null)
            {
                return false;
            }

            return await Task.Run(() =>
            {
                return !_transactionCollection.AddTransaction(
                    transaction.Amount,
                    transaction.AccountNumber,
                    transaction.CreatedDateTime,
                    transaction.TransactionType);
            });
        }

        public async Task<List<Transaction>> GetTransactionsAsync(Guid accountNumber)
        {
            return await Task.Run(() =>
            {
                return _transactionCollection.Values
                .Where(value => value != null && value.Item2.Equals(accountNumber))
                .Select(value => new Transaction { Amount = value.Item1, AccountNumber = value.Item2, CreatedDateTime = value.Item3, TransactionType = value.Item4 })                
                .ToList();
            });
        }
    }
}

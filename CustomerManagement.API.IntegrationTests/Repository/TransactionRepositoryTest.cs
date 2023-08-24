using CustomerManagement.API.Persistence;
using CustomerManagement.API.Persistence.Enums;
using CustomerManagement.API.Repository.Repositories;

namespace CustomerManagement.API.IntegrationTests.Repository
{
    [TestClass]
    public class TransactionRepositoryTest
    {
        private readonly TransactionCollection _transactionCollection;
        private readonly TransactionRepository _transactionRepository;
        public TransactionRepositoryTest() 
        {
            _transactionCollection = new TransactionCollection();
            _transactionRepository = new TransactionRepository(_transactionCollection);
        }

        [TestCleanup] 
        public void Cleanup() 
        {
            _transactionCollection.Clear();
        }

        [TestMethod]
        public async Task CreateTransactionAsync_Should_CreateTransaction()
        {
            // Arrange
            var transaction = new Transaction
            { 
                AccountNumber = Guid.NewGuid(),
                Amount = 1,
                CreatedDateTime = DateTime.UtcNow,
                TransactionType = TransactionType.Debit
            };

            // Act
            var result = await _transactionRepository.CreateTransactionAsync(transaction);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetTransactionsAsync_Should_ReturnTransactionsForAccount()
        {
            // Arrange
            var accountNumber = Guid.NewGuid();
            _transactionCollection.AddTransaction(1, accountNumber, DateTime.UtcNow, TransactionType.Debit);
            _transactionCollection.AddTransaction(1, accountNumber, DateTime.UtcNow, TransactionType.Credit);

            // Act
            var result = await _transactionRepository.GetTransactionsAsync(accountNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}

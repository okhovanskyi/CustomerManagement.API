namespace CustomerManagement.API.UnitTests.Service
{
    [TestClass]
    public class TransactionServiceTest
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;

        public TransactionServiceTest()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreateTransactionAsync_Should_ThrowExceptionIfAmountIsZero()
        {
            // Arrange
            var transaction = new TransactionDto
            {
                Amount = 0
            };

            var transactionService = new TransactionService(_transactionRepositoryMock.Object);

            // Act
            await transactionService.CreateTransactionAsync(transaction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreateTransactionAsync_Should_ThrowExceptionIfTransactionAmountIsZero()
        {
            // Arrange
            var transaction = new TransactionDto();
            var transactionService = new TransactionService(_transactionRepositoryMock.Object);

            // Act
            await transactionService.CreateTransactionAsync(transaction);            
        }

        [TestMethod]
        public async Task CreateTransactionAsync_Should_CallAppropriateRepositoryMethodOnce()
        {
            // Arrange
            var transaction = new TransactionDto
            { Amount = 1 };
            var transactionService = new TransactionService(_transactionRepositoryMock.Object);

            // Act
            await transactionService.CreateTransactionAsync(transaction);

            // Arrange
            _transactionRepositoryMock.Verify(tr => tr.CreateTransactionAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [TestMethod]
        public async Task GetTransactionsAsync_Should_CallAppropriateRepositoryMethodOnce()
        {
            // Arrange
            var accountNumber = Guid.Empty;
            var transactionService = new TransactionService(_transactionRepositoryMock.Object);
            _transactionRepositoryMock.Setup(trm => trm.GetTransactionsAsync(It.Is<Guid>(argument => argument == accountNumber)))
                .ReturnsAsync(new List<Transaction>());

            // Act
            await transactionService.GetTransactionsAsync(accountNumber);

            // Arrange
            _transactionRepositoryMock.Verify(tr => tr.GetTransactionsAsync(It.Is<Guid>(argument => argument == accountNumber)), Times.Once);
        }
    }
}

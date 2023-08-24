namespace CustomerManagement.API.UnitTests.Service
{
    [TestClass]
    public class AccountServiceTest
    {
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        
        public AccountServiceTest()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();             
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreateUserAccountAsync_Should_ThrowExceptionIfUserIdLessThanZero()
        {
            // Arrange
            const long userId = -1;
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            await accountService.CreateUserAccountAsync(userId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetAccountsAsync_Should_ThrowExceptionIfUserIdLessThanZero()
        {
            // Arrange
            const long userId = -1;
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            await accountService.GetAccountsAsync(userId);
        }

        [TestMethod]
        public async Task CreateUserAccountAsync_Should_ReturnCorrectDataTransferObject()
        {
            // Arrange
            var accountModel = new Account { Id = 1, UserId = 1, AccountNumber = Guid.Empty };

            _accountRepositoryMock
                .Setup(arm => arm.CreateUserAccountAsync(It.Is<long>(argument => argument == accountModel.UserId)))
                .ReturnsAsync(accountModel);
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var result = await accountService.CreateUserAccountAsync(accountModel.UserId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(accountModel.UserId, result.UserId);
            Assert.AreEqual(accountModel.AccountNumber, result.AccountNumber);
        }

        [TestMethod]
        public async Task GetAccountsAsync_Should_ReturnCorrectDataTransferObject()
        {
            // Arrange
            const long Id = 1;
            const long userId = 1;
            var accountNumber = Guid.Empty;

            var accountList = new List<Account>
            { new Account { Id = Id, UserId = userId, AccountNumber = accountNumber } };

            _accountRepositoryMock
                .Setup(arm => arm.GetAccountsAsync(It.Is<long>(argument => argument == userId)))
                .ReturnsAsync(accountList);
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var result = await accountService.GetAccountsAsync(userId);
            var firstModel = result?.FirstOrDefault();

            // Assert
            Assert.IsNotNull(firstModel);
            Assert.AreEqual(accountNumber, firstModel.AccountNumber);
            Assert.AreEqual(0L, firstModel.Balance);
        }

        [TestMethod]
        public async Task GetAccountsAsync_Should_ReturnEmptyDtoListInCaseOfNoAccounts()
        {
            // Arrange
            const long userId = 1;
            var accountList = new List<Account>();

            _accountRepositoryMock
                .Setup(arm => arm.GetAccountsAsync(It.Is<long>(argument => argument == userId)))
                .ReturnsAsync(accountList);
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var result = await accountService.GetAccountsAsync(userId);

            // Assert
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public async Task GetAccountsAsync_Should_ReturnEmptyDtoListIfGetsNullFromRepository()
        {
            // Arrange
            const long userId = 1;
            _accountRepositoryMock
                .Setup(arm => arm.GetAccountsAsync(It.Is<long>(argument => argument == userId)));
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var result = await accountService.GetAccountsAsync(userId);

            // Assert
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public async Task CreateUserAccountAsync_Should_CallAppropriateRepositoryMethodOnce()
        {
            // Arrange
            const long userId = 1;
            _accountRepositoryMock
                .Setup(arm => arm.CreateUserAccountAsync(It.Is<long>(argument => argument == userId)));
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            await accountService.CreateUserAccountAsync(userId);

            // Assert
            _accountRepositoryMock.Verify(ar => ar.CreateUserAccountAsync(It.IsAny<long>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAccountsAsync_Should_CallAppropriateRepositoryMethodOnce()
        {
            // Arrange
            const long userId = 1;
            _accountRepositoryMock
                .Setup(arm => arm.GetAccountsAsync(It.Is<long>(argument => argument == userId)));
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            await accountService.GetAccountsAsync(userId);

            // Assert
            _accountRepositoryMock.Verify(ar => ar.GetAccountsAsync(It.IsAny<long>()), Times.Once);
        }
    }
}

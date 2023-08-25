using CustomerManagement.API.Persistence;
using CustomerManagement.API.Repository.Repositories;

namespace CustomerManagement.API.IntegrationTests.Repository
{
    [TestClass]
    public class AccountRepositoryTest
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AccountCollection _accountCollection;

        public AccountRepositoryTest()
        {
            _accountCollection = new AccountCollection();
            _accountRepository = new AccountRepository(_accountCollection);
        }

        [TestCleanup]
        public void Cleanup() 
        {
            _accountCollection.Clear();
        }

        [TestMethod]
        public async Task CreateUserAccountAsync_ShouldCreateUser()
        {
            // Arrange
            const long userId = 1;

            // Act
            var result = await _accountRepository.CreateUserAccountAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreNotEqual(Guid.Empty, result.AccountNumber);
            Assert.IsTrue(result.Id > 0);
        }

        [TestMethod]
        public async Task GetAccountsAsync_ShouldReturnCorrectAccount()
        {
            // Arrange
            const long userId = 1;
            var accountNumber = Guid.NewGuid();
            _accountCollection.AddAccount(userId, accountNumber);

            // Act
            var result = await _accountRepository.GetAccountsAsync(userId);
            var firstAccount = result?.FirstOrDefault();

            // Assert
            Assert.IsNotNull(firstAccount);
            Assert.AreNotEqual(Guid.Empty, firstAccount.AccountNumber);
        }
    }
}

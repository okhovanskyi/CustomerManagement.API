using CustomerManagement.API.Persistence;
using CustomerManagement.API.Repository.Repositories;

namespace CustomerManagement.API.IntegrationTests.Repository
{
    [TestClass]
    public class UserRepositoryTest
    {
        private readonly IUserRepository _userRepository;
        private readonly UserCollection _userCollection;

        public UserRepositoryTest()
        {
            _userCollection = new UserCollection();
            _userRepository = new UserRepository(_userCollection);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _userCollection.Clear();
        }

        [TestMethod]
        public async Task CreateUserUserAsync_ShouldCreateUser()
        {
            // Arrange
            const string TestName = "TestName";
            const string TestSurname = "TestSurname";
            var customerUid = Guid.NewGuid();
            _userCollection.AddUser(customerUid, TestName, TestSurname);

            // Act
            var result = await _userRepository.GetUserAsync(customerUid);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(TestName, result.Name);
            Assert.AreEqual(TestSurname, result.Surname);
            Assert.IsTrue(result.Id > 0);
        }
    }
}

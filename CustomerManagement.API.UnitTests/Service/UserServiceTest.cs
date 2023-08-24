namespace CustomerManagement.API.UnitTests.Service
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [TestMethod]
        public async Task GetUserAsync_Should_CallAppropriateRepositoryMethodOnce()
        {
            // Arrange
            var customerUid = Guid.Empty;
            var userService = new UserService(_userRepositoryMock.Object);

            // Act
            await userService.GetUserAsync(customerUid);

            // Assert
            _userRepositoryMock.Verify(ur => ur.GetUserAsync(It.Is<Guid>(argument => argument == customerUid)), Times.Once);
        }

        [TestMethod]
        public async Task GetUserAsync_Should_ReturnCorrectDataTransferObject()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                CustomerUid = Guid.Empty,
                Name = "TestName",
                Surname = "TestSurname",
            };
            _userRepositoryMock
                .Setup(urm => urm.GetUserAsync(It.Is<Guid>(argument => argument == user.CustomerUid)))
                .ReturnsAsync(user);

            var userService = new UserService(_userRepositoryMock.Object);

            // Act
            var result = await userService.GetUserAsync(user.CustomerUid);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.UserId);
            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.Surname, result.Surname);
        }
    }
}

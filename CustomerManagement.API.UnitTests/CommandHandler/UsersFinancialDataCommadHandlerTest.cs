using CustomerManagement.API.Command.Commands;
using CustomerManagement.API.Command.Handlers;
using System.Net;


namespace CustomerManagement.API.UnitTests.CommandHandler
{
    [TestClass]
    public class UsersFinancialDataCommadHandlerTest
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly Mock<IUserService> _userServiceMock;

        public UsersFinancialDataCommadHandlerTest()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _userServiceMock = new Mock<IUserService>();
        }

        [TestMethod]
        public async Task HandleAsync_Should_CallGetUserServiceMethodOnce()
        {
            // Arrange
            var command = new OpenNewAccountForUserCommand
            {
                CustomerUid = Guid.Empty,
                InitialCredit = 0
            };
            var handler = new UsersFinancialDataCommadHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _userServiceMock.Verify(us => us.GetUserAsync(It.Is<Guid>(argument => argument == command.CustomerUid)), Times.Once);
        }

        [TestMethod]
        public async Task HandleAsync_Should_CallCreateUserAccountServiceMethodOnce()
        {
            // Arrange
            var command = new OpenNewAccountForUserCommand
            {
                CustomerUid = Guid.Empty,
                InitialCredit = 0
            };
            var userDto = new UserDto
            {
                UserId = 0
            };

            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.CustomerUid)))
                .ReturnsAsync(userDto);
            var handler = new UsersFinancialDataCommadHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _accountServiceMock.Verify(accountService => accountService.CreateUserAccountAsync(It.Is<long>(argument => argument == userDto.UserId)), Times.Once);
        }

        [TestMethod]
        public async Task HandleAsync_Should_CallCreateTransactionServiceMethodOnce()
        {
            // Arrange
            var command = new OpenNewAccountForUserCommand
            {
                CustomerUid = Guid.Empty,
                InitialCredit = 1
            };
            var userDto = new UserDto
            {
                UserId = 1
            };

            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.CustomerUid)))
                .ReturnsAsync(userDto);
            _accountServiceMock.Setup(asm => asm.CreateUserAccountAsync(It.Is<long>(argument => argument == userDto.UserId)))
                .ReturnsAsync(new UserAccountDto());
            var handler = new UsersFinancialDataCommadHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _transactionServiceMock.Verify(ts => ts.CreateTransactionAsync(It.IsAny<TransactionDto>()), Times.Once);
        }

        [TestMethod]
        public async Task HandleAsync_Should_ReturnNotFoundResultIfUserIsNotFound()
        {
            // Arrange
            var command = new OpenNewAccountForUserCommand
            {
                CustomerUid = Guid.Empty,
                InitialCredit = 0
            };
            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.CustomerUid)));
            var handler = new UsersFinancialDataCommadHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.HttpStatusCode);
        }

        [TestMethod]
        public async Task HandleAsync_Should_ReturnBadRequestResultIfArgumentExceptionThrownFromServices()
        {
            // Arrange
            var command = new OpenNewAccountForUserCommand
            {
                CustomerUid = Guid.Empty,
                InitialCredit = 0
            };
            var userDto = new UserDto
            {
                UserId = 0
            };

            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.CustomerUid)))
                .ReturnsAsync(userDto);
            _accountServiceMock.Setup(asm => asm.CreateUserAccountAsync(It.Is<long>(argument => argument == userDto.UserId)))
                .ThrowsAsync(new ArgumentException());

            var handler = new UsersFinancialDataCommadHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.HttpStatusCode);
        }

        [TestMethod]
        public async Task HandleAsync_Should_ReturnInternalServerErrorResultIfAccountWasNotCreated()
        {
            // Arrange
            var command = new OpenNewAccountForUserCommand
            {
                CustomerUid = Guid.Empty,
                InitialCredit = 1
            };
            var userDto = new UserDto
            {
                UserId = 1
            };

            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.CustomerUid)))
                .ReturnsAsync(userDto);
            _accountServiceMock.Setup(asm => asm.CreateUserAccountAsync(It.Is<long>(argument => argument == userDto.UserId)));
            var handler = new UsersFinancialDataCommadHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.HttpStatusCode);
        }

        [TestMethod]
        public async Task HandleAsync_Should_ReturnOkResult()
        {
            // Arrange
            var command = new OpenNewAccountForUserCommand
            {
                CustomerUid = Guid.Empty,
                InitialCredit = 0
            };
            var userDto = new UserDto
            {
                UserId = 1
            };

            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.CustomerUid)))
                .ReturnsAsync(userDto);
            _accountServiceMock.Setup(asm => asm.CreateUserAccountAsync(It.Is<long>(argument => argument == userDto.UserId)))
                .ReturnsAsync(new UserAccountDto());
            var handler = new UsersFinancialDataCommadHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.HttpStatusCode);
        }

        [TestMethod]
        public async Task HandleAsync_Should_ReturnInternalServerErrorResultIfTransactionWasNotCreated()
        {
            // Arrange
            var command = new OpenNewAccountForUserCommand
            {
                CustomerUid = Guid.Empty,
                InitialCredit = 1
            };
            var userDto = new UserDto
            {
                UserId = 1
            };
            var userAccountDto = new UserAccountDto
            {
                AccountNumber = Guid.Empty
            };
            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.CustomerUid)))
                .ReturnsAsync(userDto);
            _accountServiceMock.Setup(asm => asm.CreateUserAccountAsync(It.Is<long>(argument => argument == userDto.UserId)))
                .ReturnsAsync(userAccountDto);
            _transactionServiceMock.Setup(tsm => tsm.CreateTransactionAsync(It.IsAny<TransactionDto>()))
                .ReturnsAsync(false);
            var handler = new UsersFinancialDataCommadHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.HttpStatusCode);
        }
    }
}

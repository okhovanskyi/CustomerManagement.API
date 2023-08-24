using CustomerManagement.API.Query.Handlers;
using CustomerManagement.API.Query.Queries;
using System.Net;

namespace CustomerManagement.API.UnitTests.QueryHandler
{
    [TestClass]
    public class UserFinancialDataQueryHandlerTest
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly Mock<IUserService> _userServiceMock;

        public UserFinancialDataQueryHandlerTest()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _userServiceMock = new Mock<IUserService>();
        }

        [TestMethod]
        public async Task HandleAsync_Should_CallGetUserServiceMethodOnce()
        {
            // Arrange
            var command = new GetUserFinancialDataQuery
            {
                CustomerUid = Guid.Empty                
            };
            var handler = new UserFinancialDataQueryHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _userServiceMock.Verify(us => us.GetUserAsync(It.Is<Guid>(argument => argument == command.Id)), Times.Once);
        }

        [TestMethod]
        public async Task HandleAsync_Should_ReturnCallGetTransactionAsyncForEveryAccount()
        {
            // Arrange
            var command = new GetUserFinancialDataQuery
            {
                CustomerUid = Guid.Empty
            };
            var userDto = new UserDto
            {
                UserId = 1
            };
            var userAccounts = new List<AccountBalanceDto?>
            {
                new AccountBalanceDto{ AccountNumber = Guid.Empty },
                new AccountBalanceDto{ AccountNumber = Guid.Empty },
            };
            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.Id))).
                ReturnsAsync(userDto);
            _accountServiceMock.Setup(asm => asm.GetAccountsAsync(It.Is<long>(argument => argument == userDto.UserId)))
                .ReturnsAsync(userAccounts);

            var handler = new UserFinancialDataQueryHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _transactionServiceMock.Verify(us => us.GetTransactionsAsync(It.IsAny<Guid>()), Times.Exactly(userAccounts.Count));
        }

        [TestMethod]
        public async Task HandleAsync_Should_ReturnNotFoundResultIfUserIsNotFound()
        {
            // Arrange
            var command = new GetUserFinancialDataQuery
            {
                CustomerUid = Guid.Empty
            };
            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.Id)));
            var handler = new UserFinancialDataQueryHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.HttpStatusCode);
        }

        [TestMethod]
        public async Task HandleAsync_Should_ReturnResultIfUserHasNoAccounts()
        {
            // Arrange
            var command = new GetUserFinancialDataQuery
            {
                CustomerUid = Guid.Empty
            };
            var userDto = new UserDto
            {
                UserId = 1
            };
            var userAccounts = new List<AccountBalanceDto?>();
            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.Id))).
                ReturnsAsync(userDto);
            _accountServiceMock.Setup(asm => asm.GetAccountsAsync(It.Is<long>(argument => argument == userDto.UserId)))
                .ReturnsAsync(userAccounts);
            var handler = new UserFinancialDataQueryHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.IsNotNull(result.AccountBalanceDtos);
            Assert.AreEqual(0, result.AccountBalanceDtos.Count);
        }

        [TestMethod]
        public async Task HandleAsync_Should_ReturnBadRequestResultIfArgumentExceptionThrownFromServices()
        {
            // Arrange
            var command = new GetUserFinancialDataQuery
            {
                CustomerUid = Guid.Empty
            };
            var userDto = new UserDto
            {
                UserId = 0
            };

            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.Id)))
                .ReturnsAsync(userDto);

            var handler = new UserFinancialDataQueryHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.HttpStatusCode);
        }

        [TestMethod]
        public async Task HandleAsync_Should_FilterNullTransactions()
        {
            // Arrange
            var accountNumber = Guid.NewGuid();
            var command = new GetUserFinancialDataQuery
            {
                CustomerUid = Guid.Empty
            };
            var userDto = new UserDto
            {
                UserId = 1
            };
            var userAccounts = new List<AccountBalanceDto?>
            {
                new AccountBalanceDto{ AccountNumber = accountNumber }
            };
            var accountTransactions = new List<TransactionDto?> {
                new TransactionDto { AccountNumber = accountNumber, Amount = 10, TransactionType = Persistence.Enums.TransactionType.Debit},
                null,
                new TransactionDto { AccountNumber = accountNumber, Amount = 10, TransactionType = Persistence.Enums.TransactionType.Credit}};

            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.Id))).
                ReturnsAsync(userDto);
            _accountServiceMock.Setup(asm => asm.GetAccountsAsync(It.Is<long>(argument => argument == userDto.UserId)))
                .ReturnsAsync(userAccounts);
            _transactionServiceMock.Setup(tsm => tsm.GetTransactionsAsync(It.Is<Guid>(argument => argument == accountNumber)))
                .ReturnsAsync(accountTransactions);

            var handler = new UserFinancialDataQueryHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);
            var accountBalanceDto = result.AccountBalanceDtos?.FirstOrDefault();

            // Assert
            Assert.IsNotNull(accountBalanceDto);
            Assert.AreEqual(0, accountBalanceDto.Balance);
        }

        [TestMethod]
        public async Task HandleAsync_Should_CalculateBalanceCorrectly()
        {
            // Arrange
            var accountNumber = Guid.NewGuid();
            var command = new GetUserFinancialDataQuery
            {
                CustomerUid = Guid.Empty
            };
            var userDto = new UserDto
            {
                UserId = 1
            };
            var userAccounts = new List<AccountBalanceDto?>
            {
                new AccountBalanceDto{ AccountNumber = accountNumber }
            };
            var accountTransactions = new List<TransactionDto?>{   
                new TransactionDto { AccountNumber = accountNumber, Amount = 11, TransactionType = Persistence.Enums.TransactionType.Debit},
                new TransactionDto { AccountNumber = accountNumber, Amount = 10, TransactionType = Persistence.Enums.TransactionType.Credit}};

            _userServiceMock.Setup(usm => usm.GetUserAsync(It.Is<Guid>(argument => argument == command.Id))).
                ReturnsAsync(userDto);
            _accountServiceMock.Setup(asm => asm.GetAccountsAsync(It.Is<long>(argument => argument == userDto.UserId)))
                .ReturnsAsync(userAccounts);
            _transactionServiceMock.Setup(tsm => tsm.GetTransactionsAsync(It.Is<Guid>(argument => argument == accountNumber)))
                .ReturnsAsync(accountTransactions);

            var handler = new UserFinancialDataQueryHandler(_accountServiceMock.Object, _transactionServiceMock.Object, _userServiceMock.Object);

            // Act
            var result = await handler.HandleAsync(command);
            var accountBalanceDto = result.AccountBalanceDtos?.FirstOrDefault();

            // Assert
            Assert.IsNotNull(accountBalanceDto);
            Assert.AreEqual(1, accountBalanceDto.Balance);
        }
    }
}

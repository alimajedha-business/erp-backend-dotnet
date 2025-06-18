using Moq;
using Accounting.Domain.Entities;
using Common.Infrastructure.Logging;
using Xunit;

namespace Accounting.Tests.Services;

//public class LedgerServiceTests
//{
//    [Fact]
//    public async Task CreateLedgerAsync_LogsInformation()
//    {
//        // Arrange
//        var dbContext = new AccountingDbContext(new DbContextOptionsBuilder<AccountingDbContext>()
//                .UseInMemoryDatabase("Test").Options);
//        var repository = new Repository<Ledger>(dbContext);
//        var loggerMock = new Mock<ILoggerService>();
//        var service = new LedgerService(repository, loggerMock.Object);
//        var ledger = new Ledger { AccountId = 1 };

//        // Act
//        await service.CreateLedgerAsync(ledger);

//        // Assert
//        loggerMock.Verify(
//            x => x.LogInformation(
//                It.Is<string>(m => m.Contains("Creating ledger for AccountId {AccountId} in Accounting module")),
//                It.IsAny<object[]>()),
//            Times.Once());
//    }
//}
using Moq;
using Microsoft.Extensions.Logging;
using TradingApp.Application;
using TradingApp.Application.interfaces;
using TradingApp.Domain.entities;

[TestFixture]
public class AccountServiceTests
{
    private Mock<IAccountRepository> _accountRepositoryMock;
    private Mock<ILogger<AccountService>> _loggerMock;
    private AccountService _service;

    [SetUp]
    public void Setup()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _loggerMock = new Mock<ILogger<AccountService>>();
        _service = new AccountService(_accountRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Create_ValidInput_ShouldAddAccountToRepository()
    {
        // Arrange
        string name = "TestAccount";
        decimal amount = 100m;

        // Act
        var result = await _service.Create(name, amount);

        // Assert
        Assert.That(result.Name, Is.EqualTo(name));
        Assert.That(result.Balance, Is.EqualTo(amount));
        
        _accountRepositoryMock.Verify(r => r.AddAccount(It.IsAny<Account>()), Times.Once);
    }
}

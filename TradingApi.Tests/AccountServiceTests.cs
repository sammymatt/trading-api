using Moq;
using Microsoft.Extensions.Logging;
using TradingApi.Tests;
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
        Account account = new AccountBuilder().Build();

        // Act
        var result = await _service.Create(account.Name, 50m);

        // Assert
        Assert.That(result.Name, Is.EqualTo(account.Name));
        Assert.That(result.Balance, Is.EqualTo(50m));
        
        _accountRepositoryMock.Verify(r => r.AddAccount(It.IsAny<Account>()), Times.Once);
    }

    [Test]
    public async Task Create_InvalidInput_ShouldNotAddAccountToRepository()
    {
        // Arrange
        Account account = new AccountBuilder().WithName("").Build();
        
        // Act
        var result = await _service.Create(account.Name, 0m);
        
        // Assert
        _accountRepositoryMock.Verify(
            r => r.AddAccount(It.IsAny<Account>()),
            Times.Never
        );
    }

    [Test]
    public async Task Retrieve_ValidInput_ShouldCallRepositoryAndReturnAccount()
    {
        // Arrange
        Account account = new AccountBuilder().Build();
        _accountRepositoryMock.Setup(r => r.GetAccount(account.Name)).ReturnsAsync(account);
        
        // Act
        var result = await _service.Retrieve(account.Name);
        
        // Assert   
        Assert.That(result.Name, Is.EqualTo(account.Name));
        Assert.That(result.Balance, Is.EqualTo(account.Balance));
        
        _accountRepositoryMock.Verify(r => r.GetAccount(account.Name), Times.Once);
    }

    [Test]
    public async Task Retrieve_InvalidInput_ShouldReturnNull()
    {
        // Arrange
        Account account = new AccountBuilder().WithName("").Build();
        
        // Act
        var result = await _service.Retrieve(account.Name);
        
        // Assert
        Assert.That(result, Is.Null);
        
        _accountRepositoryMock.Verify(r => r.GetAccount(account.Name), Times.Never);
    }

    [Test]
    public async Task Deposit_ValidInput_ShouldCallRepositoryAndReturnAccount()
    {
        // Arrange
        const string accountId = "123";
        decimal amount = 100m;
        _accountRepositoryMock.Setup(r => r.Deposit(accountId, amount)).ReturnsAsync(new Account(accountId, amount));
        
        // Act
        var result = await _service.Deposit(accountId, amount);
        
        // Assert
        Assert.That(result.Name, Is.EqualTo(accountId));
        Assert.That(result.Balance, Is.EqualTo(amount));
        _accountRepositoryMock.Verify(r => r.Deposit(accountId, amount), Times.Once);
    }

    [Test]
    public async Task Deposit_InvalidInput_ShouldReturnNull()
    {
        // Arrange
        const string accountId = "123";
        decimal amount = 100m;
        
        // Act
        var result = await _service.Deposit(accountId, amount);
        
        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task Withdraw_ValidInput_ShouldCallRepositoryAndReturnAccount()
    {
        // Arrange
        const string accountId = "123";
        decimal amount = 100m;
        _accountRepositoryMock.Setup(r => r.Withdraw(accountId, amount)).ReturnsAsync(new Account(accountId, 50m));
        
        // Act
        var result = await _service.Withdraw(accountId: "123", amount: 100m);
        
        // Assert
        Assert.That(result.Name, Is.EqualTo(accountId));
        Assert.That(result.Balance, Is.EqualTo(50m));
    }

    [Test]
    public async Task Withdraw_InvalidInput_ShouldReturnNull()
    {
        // Arrange
        const string accountId = "";
        decimal amount = 100m;
        
        // Act
        var result = await _service.Withdraw(accountId, amount);
        
        // Assert
        Assert.That(result, Is.Null);
    }
    
}

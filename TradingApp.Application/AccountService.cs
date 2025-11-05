using TradingApp.Application.interfaces;
using TradingApp.Domain.entities;

namespace TradingApp.Application;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<AccountService> _logger;
    
    public AccountService(IAccountRepository accountRepository, ILogger<AccountService> logger)
    {
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task<Account> Create(string name, decimal amount)
    {
        var account = new Account(name, amount);
        if (name != null && amount > 0)
        {
            await _accountRepository.AddAccount(account);
        }
        
        _logger.LogInformation("Account created successfully: {AccountId}", account.Id);
        return account;
    }

    public async Task<Account?> Retrieve(string name)
    {
        _logger.LogInformation("Account retrieval requested: {AccountName}", name);
        var account = await _accountRepository.GetAccount(name);
        
        return account;
    }

    public async Task<Account> Deposit(string accountId, decimal amount)
    {
        var account = await _accountRepository.Deposit(accountId, amount);
        
        _logger.LogInformation("Deposit amount {Amount} successful for: {AccountId}", amount, account.Id);
        return account;
    }
    
    public async Task<Account> Withdraw(string accountId, decimal amount)
    {
        var account = await _accountRepository.Withdraw(accountId, amount);
        
        _logger.LogInformation("Withdrawal amount {Amount} successful for: {AccountId}", amount, account.Id);
        return account;
    }
}
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
            _logger.LogInformation("Account created successfully: {AccountId}", account.Id);
        }
        return account;
    }

    public async Task<Account?> Retrieve(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Attempted to retrieve account with null or empty name");
            return null;
        }

        _logger.LogInformation("Account retrieval requested: {AccountName}", name);
        Account? account = await _accountRepository.GetAccount(name);

        return account;
    }

    public async Task<Account?> Deposit(string accountId, decimal amount)
    {
        var account = await _accountRepository.Deposit(accountId, amount);
        
        if (account == null)
        {
            _logger.LogInformation("Deposit amount {Amount} unsuccessful for: {AccountId} - account not found.", amount, accountId);
            return null;
        }
        
        _logger.LogInformation("Deposit amount {Amount} successful for: {AccountId}", amount, account.Id);
        return account;
    }
    
    public async Task<Account> Withdraw(string accountId, decimal amount)
    {
        var account = await _accountRepository.Withdraw(accountId, amount);
        
        if (account == null)
        {
            _logger.LogInformation("Withdraw amount {Amount} unsuccessful for: {AccountId} - account not found.", amount, accountId);
            return null;
        }
        
        _logger.LogInformation("Withdrawal amount {Amount} successful for: {AccountId}", amount, account.Id);
        return account;
    }
}
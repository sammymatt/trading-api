using TradingApp.Application.interfaces;
using TradingApp.Domain.entities;

namespace TradingApp.Application;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> Create(string name, decimal amount)
    {
        var account = new Account(name, amount);
        if (name != null && amount > 0)
        {
            await _accountRepository.AddAccount(account);
        }
        return account;
    }

    public async Task<Account?> Retrieve(string name)
    {
        var account = await _accountRepository.GetAccount(name);
        return account;
    }

    public async Task<bool> Deposit(string name, decimal amount)
    {
        var account = await _accountRepository.Deposit(name, amount);
        return true;
    }
}
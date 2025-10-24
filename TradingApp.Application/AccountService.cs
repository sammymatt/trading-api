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

    public async Task<bool> Create(string name, decimal amount)
    {
        var account = new Account(name, amount);
        await _accountRepository.AddAccount(account);
        return true;
    }

    public async Task<Account?> Retrieve(string name)
    {
        var account = await _accountRepository.GetAccount(name);
        return account;
    }

    // public bool Deposit(string name, decimal amount)
    // {
    //     if (!_accounts.ContainsKey(name))
    //         _accounts[name] = new Account(name, amount);
    //
    //     var account = _accounts[name];
    //
    //     // Delegate the business rule to Domain
    //     return account.Deposit(amount);
    // }
}
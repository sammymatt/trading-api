namespace TradingApp.Application.interfaces;

using TradingApp.Domain.entities;

public interface IAccountRepository
{
    public Task<bool> AddAccount(Account account);
    
    public Task<Account?> GetAccount(string name);
    
    public Task<bool> Deposit(String name, decimal amount);
}
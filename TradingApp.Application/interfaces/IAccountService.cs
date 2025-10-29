using TradingApp.Domain.entities;

namespace TradingApp.Application.interfaces;

public interface IAccountService
{
    public Task<Account> Create(string name, decimal amount);
    public Task<Account> Retrieve(string name);
    public Task<Account> Deposit(string accountId, decimal amount);
}
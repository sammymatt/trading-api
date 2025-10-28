using TradingApp.Domain.entities;

namespace TradingApp.Application.interfaces;

public interface IAccountService
{
    public Task<bool> Create(string name, decimal amount);
    public Task<Account> Retrieve(string name);
    public Task<bool> Deposit(string accountId, decimal amount);
}
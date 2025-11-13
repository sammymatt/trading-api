using TradingApp.Domain.entities;

namespace TradingApi.Tests;

public class AccountBuilder
{
    private string _accountId = "1234";
    private decimal _balance = 100;
    private string _name = "TestName";

    public AccountBuilder WithAccountId(string accountId)
    {
        _accountId = accountId;
        return this;
    }

    public AccountBuilder WithBalance(decimal balance)
    {
        _balance = balance;
        return this;
    }

    public AccountBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public Account Build()
    {
        return new Account(_name, _balance);
    }
}
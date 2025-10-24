namespace TradingApp.Domain.entities;

public class Account
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }

    public Account(string name, decimal balance)
    {
        Id = Guid.NewGuid();
        Name = name;
        Balance = balance;
    }

    public bool Deposit(decimal amount)
    {
        if (amount <= 0)
            return false;
        Balance += amount;
        return true;
    }
}
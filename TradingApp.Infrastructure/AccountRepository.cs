using Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using TradingApp.Application.interfaces;
using TradingApp.Domain.entities;

namespace TradingApp.Infrastructure;

public class AccountRepository : IAccountRepository
{
    private readonly TradingDbContext _context;

    public AccountRepository(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAccount(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        IQueryable data = _context.Accounts;
        
        return true;
    }

    public async Task<Account?> GetAccount(string name)
    {
        IQueryable<Account> query = _context.Accounts.Where(a => a.Name == name);
        return await query.FirstOrDefaultAsync();
    }
}
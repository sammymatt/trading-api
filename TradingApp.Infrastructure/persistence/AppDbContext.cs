using Microsoft.EntityFrameworkCore;
using TradingApp.Domain.entities;

namespace Infrastructure.persistence;

public class TradingDbContext : DbContext
{
    public TradingDbContext(DbContextOptions<TradingDbContext> options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
}

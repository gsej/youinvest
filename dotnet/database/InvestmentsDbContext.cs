using database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace database;

public class InvestmentsDbContextFactory : IDesignTimeDbContextFactory<InvestmentsDbContext>
{
    public InvestmentsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InvestmentsDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=investments;Persist Security Info=False;User ID=sa;Password=Password123!;MultipleActiveResultSets=False;Encrypt=True;Trust Server Certificate=True;Connection Timeout=30;");
        
        return new InvestmentsDbContext(optionsBuilder.Options);
    }
}

public class InvestmentsDbContext : DbContext
{
    public InvestmentsDbContext(  DbContextOptions<InvestmentsDbContext> options) : base(options)
    {
    }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<CashStatementItem> CashStatementItems { get; set; }
    public DbSet<StockTransaction> StockTransactions { get; set; }
    public DbSet<StockPrice> StockPrices { get; set; }
    public DbSet<KnownValue> KnownValues { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CashStatementItem>()
             .Property(s => s.CashStatementItemId).HasDefaultValueSql("newid()");

        modelBuilder.Entity<StockTransaction>()
            .Property(s => s.StockTransactionId).HasDefaultValueSql("newid()");
        
        modelBuilder.Entity<KnownValue>()
            .Property(s => s.KnownValueId).HasDefaultValueSql("newid()");
    }
}
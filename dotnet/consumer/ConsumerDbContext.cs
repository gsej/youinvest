
using consumer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace consumer;

public class ConsumerDbContextFactory : IDesignTimeDbContextFactory<ConsumerDbContext>
{
    public ConsumerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ConsumerDbContext>();
        optionsBuilder.UseSqlServer("Server=tcp:gsej-youinvest-mssqlserver.database.windows.net,1433;Initial Catalog=youinvest;Persist Security Info=False;User ID=gsej;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        

        return new ConsumerDbContext(optionsBuilder.Options);
    }
}

public class ConsumerDbContext : DbContext
{
    public ConsumerDbContext(  DbContextOptions<ConsumerDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<CashStatementItem> CashStatementItems { get; set; }
    public DbSet<StockTransaction> StockTransactions { get; set; }
    public DbSet<Dividends> Dividends { get; set; }
    public DbSet<Contributions> Contributions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CashStatementItem>()
            .Property(s => s.CashStatementItemId).HasDefaultValueSql("newid()");

        modelBuilder.Entity<CashStatementItem>()
            .ToTable("CashStatementItem")
            .HasKey(c => new { c.CashStatementItemId});

        modelBuilder.Entity<StockTransaction>()
            .Property(s => s.StockTransactionId).HasDefaultValueSql("newid()");

        modelBuilder.Entity<StockTransaction>()
            .ToTable("StockTransaction")
            .HasKey(c => new { c.StockTransactionId });
        
        modelBuilder.Entity<Dividends>()
            .ToTable("Dividends")
            .HasKey(d => new { d.Account, d.Year });
        
        modelBuilder.Entity<Contributions>()
            .ToTable("Contributions")
            .HasKey(d => new { d.Account, d.Year });

    }
}
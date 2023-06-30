using consumer.Entities;
using Microsoft.EntityFrameworkCore;

namespace consumer.Database;

public class ConsumerDbContext : DbContext
{
    public ConsumerDbContext(  DbContextOptions<ConsumerDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<CashStatementItem> CashStatementItems { get; set; }
    public DbSet<StockTransaction> StockTransactions { get; set; }
    public DbSet<Dividends> Dividends { get; set; }
    public DbSet<Contributions> Contributions { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    
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
        
        modelBuilder.Entity<Stock>()
            .ToTable("Stock")
            .HasKey(s =>  s.StockId );
        
        modelBuilder.Entity<Stock>()
            .Property(s => s.StockId).HasDefaultValueSql("newid()");
    }
}
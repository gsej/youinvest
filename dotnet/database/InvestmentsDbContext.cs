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
    
    public DbSet<CashStatementItem> CashStatementItems { get; set; }
    // public DbSet<StockTransaction> StockTransactions { get; set; }
    // public DbSet<Dividends> Dividends { get; set; }
    // public DbSet<Contributions> Contributions { get; set; }
    //
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<CashStatementItem>()
             .Property(s => s.CashStatementItemId).HasDefaultValueSql("newid()");
        
         modelBuilder.Entity<CashStatementItem>()
             .ToTable("CashStatementItem")
             .HasKey(c => new { c.CashStatementItemId});

        // modelBuilder.Entity<StockTransaction>()
        //     .Property(s => s.StockTransactionId).HasDefaultValueSql("newid()");
        //
        // modelBuilder.Entity<StockTransaction>()
        //     .ToTable("StockTransaction")
        //     .HasKey(c => new { c.StockTransactionId });
        //
        // modelBuilder.Entity<Dividends>()
        //     .ToTable("Dividends")
        //     .HasKey(d => new { d.Account, d.Year });
        //
        // modelBuilder.Entity<Contributions>()
        //     .ToTable("Contributions")
        //     .HasKey(d => new { d.Account, d.Year });

    }
}
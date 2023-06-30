using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace consumer.Database;

public class ConsumerDbContextFactory : IDesignTimeDbContextFactory<ConsumerDbContext>
{
    public ConsumerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ConsumerDbContext>();
        optionsBuilder.UseSqlServer("Server=tcp:gsej-youinvest-mssqlserver.database.windows.net,1433;Initial Catalog=youinvest;Persist Security Info=False;User ID=gsej;Password=jackthehero5!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        
        return new ConsumerDbContext(optionsBuilder.Options);
    }
}
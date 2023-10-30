using api.QueryHandlers;
using database;
using database.Entities;
using Microsoft.EntityFrameworkCore;

namespace unit_tests;

public class StockPriceQueryHandlerTests
{
    private IStockPriceQueryHandler _stockPriceQueryHandler;
    private InvestmentsDbContext _context;


    public StockPriceQueryHandlerTests()
    {
        var options = new DbContextOptionsBuilder<InvestmentsDbContext>()
            .UseInMemoryDatabase(databaseName: "Investments")
            .Options;

        _context = new InvestmentsDbContext(options);

        _stockPriceQueryHandler = new StockPriceQueryQueryHandler(_context);
    }
    
    [Fact]
    public void HandleReturnsStockPrice_WithAge()
    {

       // _context.Stocks.Add(new Stock());
        // act


    }
}
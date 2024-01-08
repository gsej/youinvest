using AjBell;
using database;
using KnownValue = database.Entities.KnownValue;

namespace loader;

public class KnownValueLoader
{
    private readonly InvestmentsDbContext _context;
    private readonly IKnownValueReader _reader;

    public KnownValueLoader(InvestmentsDbContext context, IKnownValueReader reader)
    {
        _context = context;
        _reader = reader;
    }

    public async Task LoadFile(string fileName)
    {
        var knownValues = _reader.Read(fileName).ToList();

        foreach (var knownValueDto in knownValues)
        {
            decimal totalValue;
            
            var totalValueParsable = decimal.TryParse(knownValueDto.TotalValue, null, out totalValue);
            
            if (!totalValueParsable)
            {
                continue;
            }

            // var matchingAccount = accounts.SingleOrDefault(a =>
            //     a.AccountCode.Equals(balanceDto.AccountCode, StringComparison.InvariantCultureIgnoreCase));


            var date = DateTime.ParseExact(knownValueDto.Date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var formattedDate = date.ToString("yyyy-MM-dd");

            var knownValue = new KnownValue(
                accountCode: knownValueDto.AccountCode,
                date: formattedDate,
                totalValue: totalValue);

            _context.KnownValues.Add(knownValue);
            await _context.SaveChangesAsync();
        }
    }
}
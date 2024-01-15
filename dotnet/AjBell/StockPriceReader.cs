using System.Text.Json;
using System.Text.Json.Serialization;

namespace AjBell;

public class StringConverter : System.Text.Json.Serialization.JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            var stringValue = reader.GetDecimal();
            return stringValue.ToString();
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString();
        }
 
        throw new System.Text.Json.JsonException();
    }
 
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
 
}




public class StockPriceReader : IStockPriceReader
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public StockPriceReader()
    {
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        _options.Converters.Add(new StringConverter());
              
    }
    
  
    

    public IEnumerable<StockPrice> Read(string fileName)
    {
        return ReadFile(fileName);
    }
    
    public IEnumerable<StockPrice> Read()
    {
        var files = new[]
        {
            "/home/gsej/repos/share-prices/price-fetcher/legacy-prices/prices-ci.json",
            "/home/gsej/repos/share-prices/price-fetcher/legacy-prices/prices-pi.json",
        };

        var allItems = new List<StockPrice>();
        
        foreach (var fileName in files)
        {
            var items = ReadFile(fileName);
            allItems.AddRange(items);
        }

        return allItems;
    }

    private IList<StockPrice> ReadFile(string fileName)
    {
        var jsonString = File.ReadAllText(fileName);

        if (jsonString[^1] == ',')
        {
            jsonString = jsonString[0..^1];
        }

        var items = JsonSerializer.Deserialize<IList<StockPrice>>(jsonString, _options);
        return items;
    }
}
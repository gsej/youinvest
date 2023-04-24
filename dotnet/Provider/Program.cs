using System.Text.Json;
using System.Text.Json.Serialization;

namespace Provider;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");


        string fileName = "/home/gsej/repos/youinvest-csv-files/gsej-sipp/transactions.json";
        string jsonString = File.ReadAllText(fileName);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var x = JsonSerializer.Deserialize<IList<StockTransaction>>(jsonString, options);


    }
}

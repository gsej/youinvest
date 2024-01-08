using System.Text.Json;

namespace AjBell;

public class KnownValueReader : IKnownValueReader
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public IEnumerable<KnownValue> Read(string fileName)
    {
        return ReadFile(fileName);
    }

    private IList<KnownValue> ReadFile(string fileName)
    {
        var jsonString = File.ReadAllText(fileName);
        var items = JsonSerializer.Deserialize<IList<KnownValue>>(jsonString, _options);
        return items;
    }
}
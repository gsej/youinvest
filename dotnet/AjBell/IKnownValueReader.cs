namespace AjBell;

public interface IKnownValueReader
{
    IEnumerable<KnownValue> Read(string fileName);
}
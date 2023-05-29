namespace consumer;

/// <summary>
/// Will consume messages and summarize dividends
/// </summary>
class Program
{
    private const string NamespaceConnectionString =
        "Endpoint=sb://youinvest-servicebus-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=dA1+GzrFPZcGJr6wfPIfV/OPxE9lXUPgQ+ASbNCaC8c=";

    private const string DatabaseConnectionString =
        "Server=tcp:gsej-youinvest-mssqlserver.database.windows.net,1433;Initial Catalog=youinvest;Persist Security Info=False;User ID=gsej;Password=jackthehero5!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    private const string QueueName = "cashstatement-items-queue";

    static async Task Main(string[] args)
    {
        var processor = new CashStatementItemProcessor(NamespaceConnectionString, QueueName, DatabaseConnectionString);
        await processor.Process();
    }
}
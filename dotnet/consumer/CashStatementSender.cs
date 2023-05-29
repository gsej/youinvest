using common;

namespace consumer;

public class CashStatementSender : QueueSender<StockTransaction>
{
    private const string QueueName = "cashstatement-items-queue";
    public CashStatementSender() : base(QueueName)
    {
    }
}
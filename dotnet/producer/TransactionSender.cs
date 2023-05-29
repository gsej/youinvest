using common;

namespace producer;

public class TransactionSender : QueueSender<StockTransaction>
{
    private const string QueueName = "transactions-queue";
    public TransactionSender() : base(QueueName)
    {
    }
}
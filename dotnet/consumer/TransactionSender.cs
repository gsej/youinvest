using common;

namespace consumer;

public class TransactionSender : QueueSender<StockTransaction>
{
    private const string QueueName = "transactions-queue";
    public TransactionSender() : base(QueueName)
    {
    }
}
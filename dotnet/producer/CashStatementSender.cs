using common;

namespace producer;

public class CashStatementSender : QueueSender<CashStatementItem>
{
    private const string QueueName = "cashstatement-items-queue";
    public CashStatementSender() : base(QueueName)
    {
    }
}
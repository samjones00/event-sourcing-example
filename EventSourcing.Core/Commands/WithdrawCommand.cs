using EventSourcing.Core.Models;

namespace EventSourcing.Core.Commands
{
    public class DepositCommand : TransactionCommand
    {
        public DepositCommand(TransactionItem item)
        {
            Type = TransactionType.Withdraw;
            Item = item;
        }
    }
}

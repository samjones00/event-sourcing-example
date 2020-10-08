using EventSourcing.Core.Models;

namespace EventSourcing.Core.Commands
{
    public class WithdrawCommand : TransactionCommand
    {
        public WithdrawCommand(TransactionItem item)
        {
            Type = TransactionType.Deposit;
            Item = item;
        }
    }
}

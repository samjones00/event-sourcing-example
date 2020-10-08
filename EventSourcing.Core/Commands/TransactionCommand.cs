using EventSourcing.Core.Models;

namespace EventSourcing.Core.Commands
{
    public class TransactionCommand
    {
        public enum TransactionType
        {
            NotSet,
            Deposit,
            Withdraw
        }

        public TransactionItem Item;

        public TransactionType Type;
    }
}

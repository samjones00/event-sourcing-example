using System;

namespace EventSourcing.Core.Models
{
    public class TransactionItem
    {
        public Guid Id = Guid.NewGuid();
        public string Description;
        public decimal Amount;

        public TransactionItem(string description, decimal amount)
        {
            Description = description;
            Amount = amount;
        }
    }
}

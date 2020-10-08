using EventSourcing.Core.Commands;
using EventSourcing.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcing.Core
{
    [Serializable]
    public class Statement
    {
        public EventStore Store;

        public Dictionary<Guid, TransactionItem> TransactionItems = new Dictionary<Guid, TransactionItem>();

        public Statement()
        {
        }

        public Statement(EventStore store)
        {
            Store = store;
        }

        public decimal Balance => TransactionItems.Sum(x => x.Value.Amount);

        public void Deposit(TransactionItem item)
        {
            TransactionItems.Add(item.Id, item);
            Store.Add(new DepositCommand(item));
        }

        public void Withdraw(TransactionItem item)
        {
            item.Amount = item.Amount * -1;
            TransactionItems.Add(item.Id, item);
            Store.Add(new WithdrawCommand(item));
        }

        public void Process(TransactionCommand command)
        {
            if (command != null)
            {
                var item = command.Item;
                TransactionItems.Add(command.Item.Id, item);
            }
        }
    }
}

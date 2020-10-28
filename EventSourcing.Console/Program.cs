using System;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Core;
using EventSourcing.Core.Commands;
using EventSourcing.Core.Models;

namespace EventSourcing.Console
{
    class Program
    {
        private static Statement originalStatement;
        private static Statement recreatedStatement;
        private static EventStore store;

        static async Task Main(string[] args)
        {
            var orderId = Guid.Empty;

            orderId = new Guid("0e71e84e-bb2b-427a-9417-bb7f10ed00cf");
            
            var orderRequest = new CreateOrderHeaderCommand();
            var orderRequestHandler = new CreateOrderHeaderCommandHandler();
            orderId = await orderRequestHandler.Handle(orderRequest, new CancellationToken());
            
            var addRequest = new AddOrderLineItemCommand(orderId, 1, "socks", 2.99m);
            var addRequestHandler = new AddOrderLineItemCommandHandler();
            await addRequestHandler.Handle(addRequest, new CancellationToken());

            store = new EventStore();

            var statementFilename = "statement.json";
            var storeFilename = "store.json";

            originalStatement = new Statement(store);
            originalStatement.Withdraw(new TransactionItem("beer", 3.00m));
            originalStatement.Deposit(new TransactionItem("got paid", 3000.00m));
            originalStatement.Withdraw(new TransactionItem("bought dinner", 20.00m));

            DataService.Save(originalStatement, statementFilename);
            recreatedStatement = DataService.Load<Statement>(statementFilename);


            DataService.Save(store, storeFilename);
            var savedStore = DataService.Load<EventStore>(storeFilename); 
            recreatedStatement = DataService.LoadStatementFromDataStore(savedStore);
        }
    }
}

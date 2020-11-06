using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Core;
using EventSourcing.Core.Commands;
using EventSourcing.Core.Models;
using EventSourcing.Core.Repositories;

namespace EventSourcing.Console
{
    class Program
    {
        private static Statement originalStatement;
        private static Statement recreatedStatement;
        private static EventStore store;

        delegate double ArithmeticOperation(double operand1, double operand2);
        static double Addition(double number1, double number2)
        {
            return number1 + number2;
        }

        static double Multiply(double number1, double number2)
        {
            return number1 * number2;
        }
       
        static async Task Main(string[] args)
        {
            //ArithmeticOperation sum = Multiply;

            var eventStoreRepository = new EventStoreRepository();

            ArithmeticOperation operations = Addition;
            operations += Multiply;


            var sumAddition = operations(1, 2);
            var version = 1;
            var orderId = Guid.Empty;

            orderId = new Guid("0e71e84e-bb2b-427a-9417-bb7f10ed00cf");
            
            var createOrderRequest = new CreateOrderHeaderCommand();
            var orderRequestHandler = new CreateOrderHeaderCommandHandler();
            orderId = await orderRequestHandler.Handle(createOrderRequest, new CancellationToken());
            
            eventStoreRepository.Add(createOrderRequest, version);

            var addOrderLineRequest = new AddOrderLineItemCommand(orderId, 1, "socks", 2.99m);
            var addRequestHandler = new AddOrderLineItemCommandHandler();
            await addRequestHandler.Handle(addOrderLineRequest, new CancellationToken());

            eventStoreRepository.Add(addOrderLineRequest, version++);
            
            var processPaymentRequest = new ProcessPaymentCommand(orderId,"barclays");;
            var processPaymentHandler = new ProcessPaymentCommandHandler();

            //await processPaymentHandler.Handle(processPaymentRequest, new CancellationToken());

            eventStoreRepository.Add(processPaymentRequest, version++);
            eventStoreRepository.Save();

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

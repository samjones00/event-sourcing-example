using EventSourcing.Core;
using EventSourcing.Core.Models;

namespace EventSourcing.Console
{
    class Program
    {
        private static Statement originalStatement;
        private static Statement recreatedStatement;
        private static EventStore store;

        static void Main(string[] args)
        {
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

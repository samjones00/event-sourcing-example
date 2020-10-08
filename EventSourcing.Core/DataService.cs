using EventSourcing.Core.Models;
using Newtonsoft.Json;

namespace EventSourcing.Core
{
    public class DataService
    {
        public static Statement LoadStatementFromDataStore(EventStore store)
        {
            var statement = new Statement(store);

            foreach (var item in store)
            {
                statement.Process(item);
            }

            return statement;
        }

        public static void Save<T>(T statement, string filename) => System.IO.File.WriteAllText(filename, JsonConvert.SerializeObject(statement));

        public static T Load<T>(string filename)
        {
            string json = System.IO.File.ReadAllText(filename);

            var result =  JsonConvert.DeserializeObject<T>(json);

            return result;
        }
    }
}

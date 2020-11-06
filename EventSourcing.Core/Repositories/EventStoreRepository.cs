using System.Collections.Generic;
using System.IO;
using System.Linq;
using EventSourcing.Core.Factories;
using EventSourcing.Core.Interfaces;
using EventSourcing.Core.Models;
using MediatR;
using Newtonsoft.Json;

namespace EventSourcing.Core.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly List<EventStoreItem> _items = new List<EventStoreItem>();

        private readonly string _filename;

        private readonly EventStoreItemFactory _factory;

        public EventStoreRepository()
        {
            _factory = new EventStoreItemFactory();

            _filename = "EventStoreDB.json";

            if (File.Exists(_filename))
            {
                string json = File.ReadAllText(_filename);
                _items = JsonConvert.DeserializeObject<IEnumerable<EventStoreItem>>(json).ToList();
            }
            else
            {
                File.WriteAllText(_filename, JsonConvert.SerializeObject(_items));
            }
        }

        public void Add(IBaseRequest item, int version)
        {
            var @event = _factory.Create(item, version);

            _items.Add(@event);
        }

        public void Save()
        {
            File.WriteAllText(_filename, JsonConvert.SerializeObject(_items));
        }
    }
}

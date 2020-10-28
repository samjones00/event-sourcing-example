using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EventSourcing.Core.Interfaces;
using EventSourcing.Core.Models;
using Newtonsoft.Json;

namespace EventSourcing.Core.Repositories
{
    public class OrderHeaderRepository:IRepository<OrderHeader>
    {
        private readonly List<OrderHeader> _items = new List<OrderHeader>();

        private readonly string _filename;
        public OrderHeaderRepository()
        {
            _filename = "OrderHeaderDB.json";

            if (File.Exists(_filename))
            {
                string json = System.IO.File.ReadAllText(_filename);
                _items = JsonConvert.DeserializeObject<IEnumerable<OrderHeader>>(json).ToList();
            }
            else
            {
                File.WriteAllText(_filename, JsonConvert.SerializeObject(_items));
            }
        }

        public void Add(OrderHeader item)
        {
            _items.Add(item);
        }

        public void Update(OrderHeader item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            var item = Get(id);

            _items.Remove(item);
        }

        public OrderHeader Get(Guid id)
        {
            return _items.FirstOrDefault(x => x.Id.Equals(id));
        }

        public List<OrderHeader> Get()
        {
            return _items;
        }

        public void Save()
        {
            File.WriteAllText(_filename, JsonConvert.SerializeObject(_items));
        }
    }
}

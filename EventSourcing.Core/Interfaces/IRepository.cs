using System;

namespace EventSourcing.Core.Interfaces
{
    public interface IRepository<T>
    {
        public void Add(T item);
        public void Update(T item);
        public void Delete(Guid id);
        public T Get(Guid id);
        public void Save();
    }
}

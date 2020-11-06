using System;

namespace EventSourcing.Core.Interfaces
{
    public interface IWriteRepository<T>
    {
        public void Add(T item);
        public void Save();
    }
}

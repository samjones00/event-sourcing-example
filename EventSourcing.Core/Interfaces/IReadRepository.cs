using System;

namespace EventSourcing.Core.Interfaces
{
    public interface IReadRepository<T>
    {
        public T Get(Guid id);
    }
}

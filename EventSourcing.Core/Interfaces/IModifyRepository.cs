using System;

namespace EventSourcing.Core.Interfaces
{
    interface IModifyRepository<T>
    {
        public void Update(T item);
        public void Delete(Guid id);
    }
}

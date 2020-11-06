using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EventSourcing.Core.Interfaces
{
    public interface IEventStoreRepository
    {
        void Add(IBaseRequest item, int version);
        void Save();
    }
}

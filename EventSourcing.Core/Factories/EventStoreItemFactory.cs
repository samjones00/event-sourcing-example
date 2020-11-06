using System;
using EventSourcing.Core.Models;
using MediatR;
using Newtonsoft.Json;

namespace EventSourcing.Core.Factories
{
    public class EventStoreItemFactory
    {
        public EventStoreItem Create(IBaseRequest request, int version)
        {
            var result = new EventStoreItem
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                Data = JsonConvert.SerializeObject(request),
                Type = request.GetType().Name,
                Version = version
            };

            return result;
        }

    }
}

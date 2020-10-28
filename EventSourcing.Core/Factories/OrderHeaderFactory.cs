using System;
using System.Collections.Generic;
using EventSourcing.Core.Models;

namespace EventSourcing.Core.Factories
{
    public class OrderHeaderFactory
    {
        public OrderHeader Create()
        {
            var result = new OrderHeader
            {
                Id = Guid.NewGuid(),
                DateAdded = DateTime.UtcNow,
                LineItems = new List<OrderLineItem>()
            };

            return result;
        }
    }
}

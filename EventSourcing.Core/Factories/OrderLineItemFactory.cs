using System;
using EventSourcing.Core.Models;

namespace EventSourcing.Core.Factories
{
    public class OrderLineItemFactory
    {
        public OrderLineItem Create(int quantity, string description, decimal unitPrice)
        {
            var result = new OrderLineItem
            {
                DateAdded = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Description = description,
                Quantity = quantity,
                UnitPrice = unitPrice
            };

            return result;
        }
    }
}

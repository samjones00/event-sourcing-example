using System;

namespace EventSourcing.Core.Models
{
    public struct OrderLineItem
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal GrossPrice => UnitPrice * Quantity;
    }
}

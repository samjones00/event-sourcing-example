using System;
using System.Collections.Generic;

namespace EventSourcing.Core.Models
{
    public class OrderHeader
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }
        public List<OrderLineItem> LineItems { get; set; }
    }
}

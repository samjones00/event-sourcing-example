using System;

namespace EventSourcing.Core.Models
{
    public class EventStoreItem
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
        public int Version { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }
    }
}

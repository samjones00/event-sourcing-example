using EventSourcing.Core.Commands;
using System.Collections.ObjectModel;

namespace EventSourcing.Core.Models
{
    public class EventStore : Collection<TransactionCommand>
    {
    }
}

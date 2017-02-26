using System;

namespace SimpleCqrs.Interface
{
    public interface IPersistedEvent
    {
        Guid AggregateRootId { get; }
        long Version { get; }
        IEvent Instance { get; }
    }
}
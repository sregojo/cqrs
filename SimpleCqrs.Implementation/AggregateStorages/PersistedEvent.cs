using System;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.AggregateStorages
{
    public class PersistedEvent : IPersistedEvent
    {
        public PersistedEvent(Guid aggregateRootId, long version, IEvent instance)
        {
            AggregateRootId = aggregateRootId;
            Version = version;
            Instance = instance;
        }

        public Guid AggregateRootId { get; }
        public long Version { get; }
        public IEvent Instance { get; }
    }
}
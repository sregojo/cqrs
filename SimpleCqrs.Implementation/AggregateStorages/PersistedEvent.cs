using System;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.AggregateStorages
{
    public class PersistedEvent : IPersistedEvent
    {
        public PersistedEvent(Guid aggregateRootId, long version, IEvent instance)
        {
            if (aggregateRootId == Guid.Empty)
                throw new ArgumentException("Value cannot be null", nameof(aggregateRootId));
            if (version < 0) throw new ArgumentException("Value cannot be negative", nameof(version));
            if (instance == null) throw new ArgumentException("Value cannot be null", nameof(instance));

            this.AggregateRootId = aggregateRootId;
            this.Version = version;
            this.Instance = instance;
        }

        public Guid AggregateRootId { get; }
        public long Version { get; }
        public IEvent Instance { get; }
    }
}
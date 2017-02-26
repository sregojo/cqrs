using System;
using System.Collections.Generic;

namespace SimpleCqrs.Interface
{
    public interface IAggregateRoot
    {
        Guid AggregateRootId { get; }

        long Version { get; }

        IAggregateRoot Apply(IEnumerable<IPersistedEvent> events);
    }
}
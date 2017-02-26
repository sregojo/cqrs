using System;
using System.Collections.Generic;

namespace SimpleCqrs.Interface
{
    public interface IAggregateStorage
    {
        T Load<T>(Guid id)
            where T : IAggregateRoot;

        IEnumerable<IPersistedEvent> Store<T>(T aggregate, IEnumerable<IEvent> events)
            where T : IAggregateRoot;

        T Store<T>(T aggregate)
            where T : IAggregateRoot;
    }
}
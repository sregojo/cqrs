using System;
using System.Collections.Generic;

namespace SimpleCqrs.Interface
{
    public interface IEventStorage
    {
        T Load<T>(Guid aggregateRootId)
            where T : IEventSourced;

        IEnumerable<IPersistedEvent> Store<T>(T eventSourced, IEnumerable<IEvent> events)
            where T : IEventSourced;

        T Store<T>(T eventSourced)
            where T : IEventSourced;
    }
}
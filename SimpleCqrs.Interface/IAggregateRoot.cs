using System;
using System.Collections.Generic;

namespace SimpleCqrs.Interface
{
    public interface IAggregateRoot<T> : IAggregateRoot
    {
        T Model { get; }
    }

    public interface IAggregateRoot : IEventSourced
    {
        IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(ICommand command);
    }

    public interface IEventSourced
    {
        Guid Id { get; }

        long Version { get; }

        IEventSourced Apply(IEnumerable<IPersistedEvent> events);
    }
}
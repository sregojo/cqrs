using System;

namespace SimpleCqrs.Interface
{
    public interface ICommand<T>
        where T : IAggregateRoot
    {
        Guid AggregateId { get; }
    }
}
using System;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation
{
    public abstract class CommandBase<TAggregate, TData> : Envelope<TData>, ICommand<TAggregate>
        where TAggregate : IAggregateRoot
    {
        public virtual Guid AggregateId { get; }
    }
}
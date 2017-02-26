using System;
using SimpleCqrs.Interface;

namespace Cqrs.Helpers
{
    public abstract class CommandBase<TAggregateRoot, TData> : Envelope<TData>, ICommand<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        public virtual Guid AggregateId { get; }
    }
}
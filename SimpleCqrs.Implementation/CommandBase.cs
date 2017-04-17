using System;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation
{
    public abstract class CommandBase<TAggregate, TData> : Envelope<TData>, ICommand<TAggregate>
        where TAggregate : IAggregateRoot
    {
        public Guid AggregateId {
            get
            {
                try
                {
                    return this.DoAggregateId;
                }
                catch (Exception ex)
                {
                    throw new InvalidCommandException();
                }
            }
        }

        protected abstract Guid DoAggregateId { get; }
    }

    public class InvalidCommandException : Exception
    {
    }
}
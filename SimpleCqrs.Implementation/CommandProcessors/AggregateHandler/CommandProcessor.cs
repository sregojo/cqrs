using System;
using System.Collections.Generic;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.CommandProcessors
{
    public class CommandProcessor : ICommandProcessor
    {
        protected readonly IEventStorage AggregateStorage;

        public CommandProcessor(IEventStorage aggreagateStorage)
        {
            if (aggreagateStorage == null)
                throw new ArgumentException("Value cannot be null", nameof(aggreagateStorage));

            this.AggregateStorage = aggreagateStorage;
        }

        public IEither<IEnumerable<ICommandError>, TAggregate> Process<TAggregate>(ICommand<TAggregate> command)
            where TAggregate : IAggregateRoot
        {
            var aggregateRoot = this.AggregateStorage.Load<TAggregate>(command.AggregateId);

            return aggregateRoot
                .Handle(command)
                .Right(events =>
                    (TAggregate) aggregateRoot.Apply(
                        this.AggregateStorage.Store(aggregateRoot, events)));
        }
    }
}
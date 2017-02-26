using System.Collections.Generic;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.CommandHandlerInvokers
{
    public class SecuentialCommandHandlerInvoker : ICommandHandlerInvoker
    {
        public IEnumerable<IEvent> Invoke<TAggregate, TCommand>(
            TAggregate aggregateRoot,
            TCommand command,
            IEnumerable<ICommandHandler<TAggregate, TCommand>> commandHandlers)
            where TAggregate : IAggregateRoot
            where TCommand : ICommand<TAggregate>
        {
            foreach (var commandHandler in commandHandlers)
            foreach (var @event in commandHandler.Handle(aggregateRoot, command))
                yield return @event;
        }
    }
}
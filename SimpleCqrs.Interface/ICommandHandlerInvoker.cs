using System.Collections.Generic;

namespace SimpleCqrs.Interface
{
    public interface ICommandHandlerInvoker
    {
        IEnumerable<IEvent> Invoke<TAggregate, TCommand>(TAggregate aggregateRoot, TCommand command,
            IEnumerable<ICommandHandler<TAggregate, TCommand>> commandHandlers)
            where TAggregate : IAggregateRoot
            where TCommand : ICommand<TAggregate>;
    }
}
using System.Collections.Generic;

namespace SimpleCqrs.Interface
{
    public interface ICommandHandler<TAggregateRoot, TCommand>
        where TAggregateRoot : IAggregateRoot
        where TCommand : ICommand<TAggregateRoot>
    {
        IEnumerable<IEvent> Handle(TAggregateRoot aggregateRoot, TCommand command);
    }
}
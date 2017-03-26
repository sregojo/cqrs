using System.Collections.Generic;

namespace SimpleCqrs.Interface
{
    public interface ICommandProcessor
    {
        IEither<IEnumerable<ICommandError>, TAggregate> Process<TAggregate>(ICommand<TAggregate> command)
            where TAggregate : IAggregateRoot;
    }
}
namespace SimpleCqrs.Interface
{
    public interface ICommandProcessor
    {
        void Process<TAggregate, TCommand>(TCommand command)
            where TAggregate : IAggregateRoot
            where TCommand : ICommand<TAggregate>;
    }
}
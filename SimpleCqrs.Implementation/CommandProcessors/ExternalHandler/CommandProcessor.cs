namespace SimpleCqrs.Implementation.CommandProcessors
{
    //public class CommandProcessor : ICommandProcessor

    //{
    //    protected readonly IAggregateStorage AggregateStorage;
    //    protected readonly ICommandHandlerInvoker CommandHandlerInvoker;
    //    protected readonly ICommandHandlerLocator CommandHandlerLocator;

    //    public CommandProcessor(
    //        IAggregateStorage aggreagateStorage,
    //        ICommandHandlerLocator commandHandlerLocator,
    //        ICommandHandlerInvoker commandHandlerInvoker)
    //    {
    //        if (aggreagateStorage == null)
    //            throw new ArgumentException("Value cannot be null", nameof(aggreagateStorage));
    //        if (commandHandlerLocator == null)
    //            throw new ArgumentException("Value cannot be null", nameof(commandHandlerLocator));
    //        if (commandHandlerInvoker == null)
    //            throw new ArgumentException("Value cannot be null", nameof(commandHandlerInvoker));

    //        this.CommandHandlerInvoker = commandHandlerInvoker;
    //        this.CommandHandlerLocator = commandHandlerLocator;
    //        this.AggregateStorage = aggreagateStorage;
    //    }

    //    public IEither<IEnumerable<ICommandError>, TAggregate> Process<TAggregate, TCommand>(TCommand command)
    //        where TAggregate : IAggregateRoot
    //        where TCommand : ICommand<TAggregate>
    //    {
    //        var handlers = this.CommandHandlerLocator.Locate<TAggregate, TCommand>();

    //        if (handlers.Any())
    //        {
    //            var aggregateRoot = this.AggregateStorage.Load<TAggregate>(command.AggregateId);

    //            return aggregateRoot
    //                    .Handle(command)
    //                    .Case(
    //                        errors => errors, 
    //                        events => (TAggregate)aggregateRoot.Apply(
    //                            this.AggregateStorage.Store(aggregateRoot, events)));
    //        }
    //        throw new Exception("No handlers for command");
    //    }
    //}
}
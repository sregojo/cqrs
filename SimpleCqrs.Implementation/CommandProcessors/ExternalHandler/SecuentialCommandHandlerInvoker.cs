namespace SimpleCqrs.Implementation.CommandHandlerInvokers
{
    //public class SecuentialCommandHandlerInvoker : ICommandHandlerInvoker
    //{
    //    public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Invoke<TAggregate, TCommand>(
    //        TAggregate aggregateRoot,
    //        TCommand command,
    //        IEnumerable<ICommandHandler<TAggregate, TCommand>> commandHandlers)
    //        where TAggregate : IAggregateRoot
    //        where TCommand : ICommand<TAggregate>
    //    {
    //        var handlerResult = new List<IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>>>();

    //        foreach (var commandHandler in commandHandlers)
    //        {
    //            handlerResult.Add(commandHandler.Handle(aggregateRoot, command));
    //        }

    //        return this.Unwrap(handlerResult);
    //    }

    //    private IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Unwrap(IEnumerable<IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>>> results)
    //    {
    //        var commandErrors = new List<ICommandError>();
    //        var commandEvents = new List<IEvent>();

    //        foreach (var result in results)
    //        {
    //            result.Case(
    //                errors => commandErrors.AddRange(errors),
    //                events => commandEvents.AddRange(events));
    //        }

    //        if (commandErrors.Any())
    //        {
    //            return Either.Create<IEnumerable<ICommandError>, IEnumerable<IEvent>>(commandErrors);
    //        }

    //        return Either.Create<IEnumerable<ICommandError>, IEnumerable<IEvent>>(commandEvents); ;
    //    }
    //}
}
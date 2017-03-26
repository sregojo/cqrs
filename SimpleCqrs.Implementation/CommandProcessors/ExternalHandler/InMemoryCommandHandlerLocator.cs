namespace SimpleCqrs.Implementation.CommandHandlersLocators
{
    //public class InMemoryCommandHandlerLocator : ICommandHandlerLocator
    //{
    //    protected static HashSet<Type> registrations = new HashSet<Type>();

    //    public IEnumerable<ICommandHandler<TAggregate, TCommand>> Locate<TAggregate, TCommand>()
    //        where TAggregate : IAggregateRoot
    //        where TCommand : ICommand<TAggregate>
    //    {
    //        var handlerType =
    //            registrations.Single(t => typeof(ICommandHandler<TAggregate, TCommand>).IsAssignableFrom(t));
    //        return new[]
    //        {
    //            (ICommandHandler<TAggregate, TCommand>) Activator.CreateInstance(handlerType)
    //        };
    //    }

    //    public static void Register<THandler, TAggregate, TCommand>()
    //        where THandler : ICommandHandler<TAggregate, TCommand>
    //        where TAggregate : IAggregateRoot
    //        where TCommand : ICommand<TAggregate>
    //    {
    //        registrations.Add(typeof(THandler));
    //    }
    //}
}
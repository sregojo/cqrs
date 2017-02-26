using System;
using System.Linq;
using System.Transactions;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.CommandProcessors
{
    public class TransactionalCommandProcessor : ICommandProcessor

    {
        protected readonly IAggregateStorage AggregateStorage;
        protected readonly ICommandHandlerInvoker CommandHandlerInvoker;
        protected readonly ICommandHandlerLocator CommandHandlerLocator;

        public TransactionalCommandProcessor(
            IAggregateStorage aggreagateStorage,
            ICommandHandlerLocator commandHandlerLocator,
            ICommandHandlerInvoker commandHandlerInvoker)
        {
            CommandHandlerInvoker = commandHandlerInvoker;
            CommandHandlerLocator = commandHandlerLocator;
            AggregateStorage = aggreagateStorage;
        }

        public void Process<TAggregate, TCommand>(TCommand command)
            where TAggregate : IAggregateRoot
            where TCommand : ICommand<TAggregate>
        {
            var handlers = CommandHandlerLocator.Locate<TAggregate, TCommand>();

            if (handlers.Any())
                using (var transaction = new TransactionScope())
                {
                    var aggregateRoot = AggregateStorage.Load<TAggregate>(command.AggregateId);

                    aggregateRoot.Apply(
                        AggregateStorage.Store(
                            aggregateRoot,
                            CommandHandlerInvoker.Invoke(aggregateRoot, command, handlers)));

                    transaction.Complete();
                }
            else throw new Exception("No handlers for command");
        }
    }
}
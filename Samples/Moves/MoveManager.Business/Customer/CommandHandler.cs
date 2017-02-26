using System.Collections.Generic;
using Cqrs.Helpers;
using SimpleCqrs.Interface;

namespace MoveManager.Business.Customer
{
    public class CommandHandler :
        ICommandHandler<CustomerAggregateRoot, Commands.CustomerUpdateCommand>,
        ICommandHandler<CustomerAggregateRoot, Commands.MoveUpdateCommand>,
        ICommandHandler<CustomerAggregateRoot, Commands.SurveyUpdateCommand>
    {
        public IEnumerable<IEvent> Handle(CustomerAggregateRoot aggregateRoot, Commands.CustomerUpdateCommand command)
        {
            return new Events.CustomerUpdatedEvent(command.Data).ToEnumerable();
        }

        public IEnumerable<IEvent> Handle(CustomerAggregateRoot aggregateRoot, Commands.MoveUpdateCommand command)
        {
            return new Events.MoveUpdatedEvent(command.Data).ToEnumerable();
        }

        public IEnumerable<IEvent> Handle(CustomerAggregateRoot aggregateRoot, Commands.SurveyUpdateCommand command)
        {
            return new Events.SurveyUpdatedEvent(command.Data).ToEnumerable();
        }
    }
}
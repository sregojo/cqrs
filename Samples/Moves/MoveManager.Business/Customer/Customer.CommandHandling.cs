using System.Collections.Generic;
using SimpleCqrs.Implementation;
using SimpleCqrs.Interface;

namespace MoveManager.Business.Customer
{
    partial class Customer
    {
        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(
            Model.Customer aggregateRoot,
            Commands.CustomerUpdateCommand command)
        {
            if (string.IsNullOrEmpty(command.Data.Name))
                return CommandResult.Failed("Name cannot be empty");

            return CommandResult.Handled(new Events.CustomerUpdatedEvent(command.Data));
        }

        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(
            Model.Customer aggregateRoot,
            Commands.MoveUpdateCommand command)
        {
            return CommandResult.Handled(new Events.MoveUpdatedEvent(command.Data));
        }

        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(
            Model.Customer aggregateRoot,
            Commands.SurveyUpdateCommand command)
        {
            return CommandResult.Handled(new Events.SurveyUpdatedEvent(command.Data));
        }
    }
}
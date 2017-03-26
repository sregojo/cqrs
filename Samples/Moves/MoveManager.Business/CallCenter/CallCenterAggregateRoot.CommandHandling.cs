using System;
using System.Collections.Generic;
using SimpleCqrs.Implementation;
using SimpleCqrs.Interface;

namespace MoveManager.Business.CallCenter
{
    public partial class CallCenterAggregateRoot
    {
        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(
            Model.CallCenter callcenter,
            Commands.NewAppointmentCallCommand command)
        {
            if (command.AggregateId != Guid.Empty) return CommandResult.Failed("Cannot operate on existing calls");

            return CommandResult.Handled(new Events.NewAppointmentCallEvent(command.Data));
        }
    }
}
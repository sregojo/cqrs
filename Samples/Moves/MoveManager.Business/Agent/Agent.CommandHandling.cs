using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCqrs.Implementation;
using SimpleCqrs.Interface;

namespace MoveManager.Business.Agent
{
    public partial class Agent
    {
        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(
            Model.Agent agent,
            Commands.AgentUpdateCommand command)
        {
            if (command.AggregateId == Guid.Empty)
                return CommandResult.Handled(new Events.AgentNewEvent(command.Data));

            return CommandResult.Handled(new Events.AgentUpdatedEvent(command.Data));
        }

        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(
            Model.Agent agent,
            Commands.AppointmentUpdateCommand command)
        {
            if (command.AggregateId == Guid.Empty)
                return CommandResult.Failed(
                    new AgentNotFoundError(command.AggregateId));

            if (!agent.IsValidAddress(command.Data.Address))
                return CommandResult.Failed(
                    new InvalidAddressError(command.Data.Address));

            if (!agent.IsValidSchedule(command.Data.StartDate, command.Data.EndDate))
                return CommandResult.Failed(
                    new InvalidScheduleError(
                        command.Data.StartDate,
                        command.Data.EndDate,
                        command.Data.AgentId));

            if (command.Data.AppointmentId != 0)
            {
                if (agent.HasAppointment(command.Data.AppointmentId))
                    return CommandResult.Handled(new Events.AppointmentUpdatedEvent(command.Data));

                return CommandResult.Failed(
                    new MissingAppointmentError(
                        command.Data.AppointmentId,
                        command.Data.AgentId));
            }

            if (agent.CanScheduleAppointment(command.Data.StartDate, command.Data.EndDate))
                return
                    CommandResult.Handled(new Events.AppointmentNewEvent(agent.Appointments.Count() + 1,
                        command.Data));

            return CommandResult.Failed(
                new AppointmentConflictError(
                    command.Data.StartDate,
                    command.Data.EndDate,
                    command.Data.AgentId));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Helpers;
using SimpleCqrs.Interface;

namespace MoveManager.Business.Agent
{
    public class CommandHandler :
        ICommandHandler<AgentAggregateRoot, Commands.AgentUpdateCommand>,
        ICommandHandler<AgentAggregateRoot, Commands.AppointmentUpdateCommand>
    {
        public IEnumerable<IEvent> Handle(AgentAggregateRoot agent, Commands.AgentUpdateCommand command)
        {
            if (command.AggregateId == Guid.Empty)
                return new Events.AgentNewEvent(command.Data).ToEnumerable();
            return new Events.AgentUpdatedEvent(command.Data).ToEnumerable();
        }

        public IEnumerable<IEvent> Handle(AgentAggregateRoot agent, Commands.AppointmentUpdateCommand command)
        {
            if (command.Data.AppointmentId != 0)
                if (agent.HasAppointment(command.Data.AppointmentId))
                    return new Events.AppointmentUpdatedEvent(command.Data).ToEnumerable();
                else
                    throw new Exception
                        ($"Cannot locate appointment: {command.Data.AppointmentId} for agent {command.Data.AgentId}");

            if (agent.CanScheduleAppointment(command.Data.StartDate, command.Data.EndDate))
                return new Events.AppointmentNewEvent(agent.Model.Appointments.Count() + 1, command.Data).ToEnumerable();

            throw new Exception(
                $"Cannot schedule appointment: from {command.Data.StartDate} to {command.Data.EndDate} for agent {command.Data.AgentId}");
        }
    }
}
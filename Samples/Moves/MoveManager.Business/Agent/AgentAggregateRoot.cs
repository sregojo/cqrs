using System;
using System.Linq;
using SimpleCqrs.Implementation.AggregateRoots;
using SimpleCqrs.Interface;

namespace MoveManager.Business.Agent
{
    public class AgentAggregateRoot : AggregateRootBase<Model.Agent>,
        IApplyEvent<Events.AppointmentNewEvent>,
        IApplyEvent<Events.AppointmentUpdatedEvent>,
        IApplyEvent<Events.AgentNewEvent>,
        IApplyEvent<Events.AgentUpdatedEvent>
    {
        public AgentAggregateRoot(Guid aggregateRootId) : base(aggregateRootId)
        {
        }

        public void Apply(Events.AgentNewEvent @event)
        {
            Model = @event.Data;
        }

        public void Apply(Events.AgentUpdatedEvent @event)
        {
            Model = @event.Data;
        }

        public void Apply(Events.AppointmentNewEvent @event)
        {
            Model.Appointments.Add(@event.Data.AppointmentId, @event.Data);
        }

        public void Apply(Events.AppointmentUpdatedEvent @event)
        {
            Model.Appointments[@event.Data.AppointmentId] = @event.Data;
        }

        public bool HasAppointment(long appointmentId)
        {
            return Model.Appointments.ContainsKey(appointmentId);
        }

        public bool CanScheduleAppointment(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate) return false;

            return !Model.Appointments.Values.Any(a =>
                startDate >= a.StartDate && startDate < a.EndDate
                ||
                endDate > a.StartDate && endDate <= a.EndDate);
        }
    }
}
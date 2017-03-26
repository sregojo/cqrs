namespace MoveManager.Business.Agent
{
    public partial class Agent
    {
        public void Apply(Events.AgentNewEvent @event)
        {
            this.Model = @event.Data;
        }

        public void Apply(Events.AgentUpdatedEvent @event)
        {
            this.Model.Name = @event.Data.Name;
        }

        public void Apply(Events.AppointmentNewEvent @event)
        {
            this.Model.Appointments.Add(@event.Data.AppointmentId, @event.Data);
        }

        public void Apply(Events.AppointmentUpdatedEvent @event)
        {
            this.Model.Appointments[@event.Data.AppointmentId] = @event.Data;
        }
    }
}
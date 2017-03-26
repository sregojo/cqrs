using System;

namespace MoveManager.Business.CallCenter
{
    public partial class CallCenterAggregateRoot
    {
        public void Apply(Events.NewAppointmentCallEvent @event)
        {
            var id = Guid.NewGuid();
            this.Model.Calls.Add(id, @event.Data);
        }
    }
}
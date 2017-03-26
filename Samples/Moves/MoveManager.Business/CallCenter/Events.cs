using SimpleCqrs.Implementation;

namespace MoveManager.Business.CallCenter
{
    public class Events
    {
        public class NewAppointmentCallEvent : EventBase<Model.AppointmentCall>
        {
            public NewAppointmentCallEvent(Model.AppointmentCall data)
            {
                this.Data = data;
            }
        }
    }
}
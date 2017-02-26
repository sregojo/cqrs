using Cqrs.Helpers;

namespace MoveManager.Business.Agent
{
    public class Events
    {
        public class AgentNewEvent : EventBase<Model.Agent>
        {
            public AgentNewEvent(Model.Agent data)
            {
                Data = data;
            }
        }

        public class AgentUpdatedEvent : EventBase<Model.Agent>
        {
            public AgentUpdatedEvent(Model.Agent data)
            {
                Data = data;
            }
        }

        public class AppointmentUpdatedEvent : EventBase<Model.Appointment>
        {
            public AppointmentUpdatedEvent(Model.Appointment data)
            {
                Data = data;
            }
        }

        public class AppointmentNewEvent : EventBase<Model.Appointment>
        {
            public AppointmentNewEvent(long appointmentId, Model.Appointment data)
            {
                Data = data;
                Data.AppointmentId = appointmentId;
            }
        }
    }
}
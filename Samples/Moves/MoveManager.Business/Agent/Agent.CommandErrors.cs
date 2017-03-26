using System;
using SimpleCqrs.Interface;

namespace MoveManager.Business.Agent
{
    public partial class Agent
    {
        public class MissingAppointmentError : ICommandError
        {
            public MissingAppointmentError(long appointmentId, Guid agentId)
            {
                this.AppointmentId = appointmentId;
                this.AgentId = agentId;
            }

            public long AppointmentId { get; }
            public Guid AgentId { get; }

            public string Message => $"Cannot locate appointment: {this.AppointmentId} for agent {this.AgentId}";
        }

        public class AppointmentConflictError : ICommandError
        {
            public AppointmentConflictError(DateTime startDate, DateTime endDate, Guid agentId)
            {
                this.StartDate = startDate;
                this.EndDate = endDate;
                this.AgentId = agentId;
            }

            public DateTime StartDate { get; }
            public DateTime EndDate { get; }
            public Guid AgentId { get; }

            public string Message
                => $"Cannot schedule appointment: from {this.StartDate} to {this.EndDate} for agent {this.AgentId}";
        }
    }
}
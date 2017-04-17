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

        public class InvalidScheduleError : ICommandError
        {
            public InvalidScheduleError(DateTime startDate, DateTime endDate, Guid agentId)
            {
                this.StartDate = startDate;
                this.EndDate = endDate;
                this.AgentId = agentId;
            }

            public DateTime StartDate { get; }
            public DateTime EndDate { get; }
            public Guid AgentId { get; }

            public string Message
                => $"Appointment StartDate and EndDate are not valid";
        }

        public class AgentNotFoundError : ICommandError
        {
            public AgentNotFoundError(Guid agentId)
            {
                this.AgentId = agentId;
            }
            public Guid AgentId { get; }

            public string Message
                => $"Agent {this.AgentId} not found";
        }

        public class InvalidAddressError : ICommandError
        {
            public Model.Address Address { get; }

            public InvalidAddressError(Model.Address address)
            {
                this.Address = address;
            }

            public string Message
                => $"Invalid Address";
        }
    }
}
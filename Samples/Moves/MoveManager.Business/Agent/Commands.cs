using System;
using SimpleCqrs.Implementation;

namespace MoveManager.Business.Agent
{
    public class Commands
    {
        public class AgentUpdateCommand : CommandBase<Agent, Model.Agent>
        {
            protected override Guid DoAggregateId => this.Data.AgentId;
        }

        public class AppointmentUpdateCommand : CommandBase<Agent, Model.Appointment>
        {
            public Guid CustomerId { get; set; }
            protected override Guid DoAggregateId => this.Data.AgentId;
        }
    }
}
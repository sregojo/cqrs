using System;
using SimpleCqrs.Implementation;

namespace MoveManager.Business.Agent
{
    public class Commands
    {
        public class AgentUpdateCommand : CommandBase<Agent, Model.Agent>
        {
            public override Guid AggregateId => this.Data.AgentId;
        }

        public class AppointmentUpdateCommand : CommandBase<Agent, Model.Appointment>
        {
            public override Guid AggregateId => this.Data.AgentId;
        }
    }
}
using System;
using Cqrs.Helpers;

namespace MoveManager.Business.Agent
{
    public class Commands
    {
        public class AgentUpdateCommand : CommandBase<AgentAggregateRoot, Model.Agent>
        {
            public override Guid AggregateId => Data.AgentId;
        }

        public class AppointmentUpdateCommand : CommandBase<AgentAggregateRoot, Model.Appointment>
        {
            public override Guid AggregateId => Data.AgentId;
        }
    }
}
using System;
using SimpleCqrs.Implementation.AggregateRoots;
using SimpleCqrs.Interface;

namespace MoveManager.Business.Agent
{
    public partial class Agent : AggregateRootBase<Model.Agent>,
        IApplyEvent<Events.AppointmentNewEvent>,
        IApplyEvent<Events.AppointmentUpdatedEvent>,
        IApplyEvent<Events.AgentNewEvent>,
        IApplyEvent<Events.AgentUpdatedEvent>,

        IHandleCommand<Model.Agent, Commands.AgentUpdateCommand>,
        IHandleCommand<Model.Agent, Commands.AppointmentUpdateCommand>
    {
        public Agent(Guid aggregateRootId) : base(aggregateRootId)
        {
            this.Model.AgentId = aggregateRootId;
        }
    }
}
using System;
using SimpleCqrs.Implementation.AggregateRoots;
using SimpleCqrs.Interface;

namespace MoveManager.Business.Customer
{
    public class CustomerAggregateRoot : AggregateRootBase<Model.Customer>,
        IApplyEvent<Events.CustomerUpdatedEvent>,
        IApplyEvent<Events.MoveUpdatedEvent>,
        IApplyEvent<Events.SurveyUpdatedEvent>
    {
        public CustomerAggregateRoot(Guid aggregateRootId) : base(aggregateRootId)
        {
        }

        public void Apply(Events.CustomerUpdatedEvent @event)
        {
            Model = @event.Data;
        }

        public void Apply(Events.MoveUpdatedEvent @event)
        {
            Model.Moves[@event.Data.MoveId] = @event.Data;
        }

        public void Apply(Events.SurveyUpdatedEvent @event)
        {
            Model.Moves[@event.Data.MoveId].Survey = @event.Data;
        }
    }
}
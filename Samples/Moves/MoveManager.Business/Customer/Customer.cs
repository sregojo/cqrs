using System;
using SimpleCqrs.Implementation.AggregateRoots;
using SimpleCqrs.Interface;

namespace MoveManager.Business.Customer
{
    public partial class Customer : AggregateRootBase<Model.Customer>,
        IApplyEvent<Events.CustomerUpdatedEvent>,
        IApplyEvent<Events.MoveUpdatedEvent>,
        IApplyEvent<Events.SurveyUpdatedEvent>,
        IHandleCommand<Model.Customer, Commands.CustomerUpdateCommand>,
        IHandleCommand<Model.Customer, Commands.MoveUpdateCommand>,
        IHandleCommand<Model.Customer, Commands.SurveyUpdateCommand>
    {
        public Customer(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}
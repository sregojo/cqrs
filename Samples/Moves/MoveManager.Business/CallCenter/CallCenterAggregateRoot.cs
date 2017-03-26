using System;
using SimpleCqrs.Implementation.AggregateRoots;
using SimpleCqrs.Interface;

namespace MoveManager.Business.CallCenter
{
    public partial class CallCenterAggregateRoot : AggregateRootBase<Model.CallCenter>,
        IApplyEvent<Events.NewAppointmentCallEvent>,
        IHandleCommand<Model.CallCenter, Commands.NewAppointmentCallCommand>
    {
        public CallCenterAggregateRoot(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}
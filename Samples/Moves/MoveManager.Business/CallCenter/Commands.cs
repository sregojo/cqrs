using System;
using SimpleCqrs.Implementation;

namespace MoveManager.Business.CallCenter
{
    public class Commands
    {
        public class NewAppointmentCallCommand : CommandBase<CallCenterAggregateRoot, Model.AppointmentCall>
        {
            public override Guid AggregateId => Guid.Empty;
        }
    }
}
using System;
using SimpleCqrs.Implementation;

namespace MoveManager.Business.CallCenter
{
    public class Commands
    {
        public class NewAppointmentCallCommand : CommandBase<CallCenterAggregateRoot, Model.AppointmentCall>
        {
            protected override Guid DoAggregateId => Guid.Empty;
        }
    }
}
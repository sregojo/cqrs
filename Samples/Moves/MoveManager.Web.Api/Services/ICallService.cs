using System.Collections.Generic;
using MoveManager.Web.Api.Models;
using SimpleCqrs.Interface;

namespace MoveManager.Web.Api.Services
{
    public interface ICallService
    {
        IEither<IEnumerable<ICommandError>, NewAppointmentResponse> NewAppointmentCall(
            NewAppointmentCallRequest newAppointmentCall);
    }
}
using System;
using System.Web.Http;
using MoveManager.Web.Api.Models;
using MoveManager.Web.Api.Services;

namespace MoveManager.Web.Api.Controllers
{
    public class CallController : ApiController
    {
        private readonly ICallService CallService;

        public CallController(ICallService callService)
        {
            this.CallService = callService;
        }

        [Route("Call/Appointment")]
        [HttpPost]
        public IHttpActionResult PostAppointmentCall(NewAppointmentCallRequest newAppointmentCall)
        {
            try
            {
                return this.CallService.NewAppointmentCall(newAppointmentCall)
                    .Case<IHttpActionResult>(
                        errors => this.Conflict(),
                        newAppointmentResponse => this.Ok(newAppointmentResponse));
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }
    }
}
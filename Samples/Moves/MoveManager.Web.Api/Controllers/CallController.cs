using System;
using System.Linq.Expressions;
using System.Web.Http;
using MoveManager.Web.Api.Models;
using MoveManager.Web.Api.Services;
using SimpleCqrs.Implementation;
using SimpleCqrs.Implementation.AggregateStorages.SqlServer;

namespace MoveManager.Web.Api.Controllers
{
    public class CallController : ApiController
    {
        private readonly ICallService CallService;

        public CallController(ICallService callService)
        {
            this.CallService = callService;
        }

        [Route("call/appointment")]
        [HttpPost]
        public IHttpActionResult PostCallAppointment(NewAppointmentCallRequest newAppointmentCall)
        {
            try
            {
                return this.CallService.NewAppointmentCall(newAppointmentCall)
                    .Case<IHttpActionResult>(
                        errors => this.Conflict(),
                        newAppointmentResponse => this.Ok(newAppointmentResponse));
            }
            catch (InvalidCommandException)
            {
                return this.BadRequest();
            }
            catch (NotExistingAggregate)
            {
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }
    }
}
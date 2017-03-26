using MoveManager.Business;

namespace MoveManager.Web.Api.Models
{
    public class NewAppointmentCallRequest
    {
        public Model.Customer Customer { get; set; }
        public Model.Appointment Appointment { get; set; }
    }
}
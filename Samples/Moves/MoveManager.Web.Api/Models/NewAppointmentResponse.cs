using MoveManager.Business.Agent;
using MoveManager.Business.Customer;

namespace MoveManager.Web.Api.Models
{
    public class NewAppointmentResponse
    {
        public Customer customer { get; set; }
        public Agent agent { get; set; }
    }
}
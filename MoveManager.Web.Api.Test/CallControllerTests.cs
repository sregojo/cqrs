using System;
using System.Net;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoveManager.Business;
using MoveManager.Web.Api.Models;

namespace MoveManager.Web.Api.Test
{
    [TestClass]
    public class CallControllerTests
    {
        [TestMethod]
        public void PostCallAppointment_Empty_Customer_BadRequest()
        {
            using (var server = TestServer.Create<MoveManager.Web.Api.App_Start.Startup>())
            {
                var request = new NewAppointmentCallRequest()
                {
                };

                var response = server.PostRequest("call/appointment", request);

                response.AssertIs(
                    HttpStatusCode.BadRequest,
                    new CommandError() {Message = "Invalid Command"});
            }
        }

        [TestMethod]
        public void PostCallAppointment_NotExisting_Customer_BadRequest()
        {
            using (var server = TestServer.Create<MoveManager.Web.Api.App_Start.Startup>())
            {
                var request = new NewAppointmentCallRequest()
                {
                    Customer = new Model.Customer()
                    {
                        CustomerId = Guid.NewGuid(),
                    }
                };

                var response = server.PostRequest("call/appointment", request);

                response.AssertIs(
                    HttpStatusCode.BadRequest,
                    new CommandError() { Message = $"Not existing entity {request.Customer.CustomerId}" });
            }
        }

        [TestMethod]
        public void PostCallAppointment_New_Customer_Empty_Name_Conflict()
        {
            using (var server = TestServer.Create<MoveManager.Web.Api.App_Start.Startup>())
            {
                var request = new NewAppointmentCallRequest()
                {
                    Customer = new Model.Customer()
                    {
                        CustomerId = Guid.Empty,
                        Name = string.Empty,
                    }
                };

                var response = server.PostRequest("call/appointment", request);

                response.AssertIs(
                    HttpStatusCode.Conflict,
                    new CommandError() { Message = "Name cannot be empty" });
            }
        }

        [TestMethod]
        public void PostCallAppointment_New_Customer_Null_Name_Conflict()
        {
            using (var server = TestServer.Create<MoveManager.Web.Api.App_Start.Startup>())
            {
                var request = new NewAppointmentCallRequest()
                {
                    Customer = new Model.Customer()
                    {
                        CustomerId = Guid.Empty,
                        Name = null,
                    }
                };

                var response = server.PostRequest("call/appointment", request);

                response.AssertIs(
                    HttpStatusCode.Conflict,
                    new CommandError() { Message = "Name cannot be empty" });
            }
        }

        [TestMethod]
        public void PostCallAppointment_Empty_Appointment_BadRequest()
        {
            using (var server = TestServer.Create<MoveManager.Web.Api.App_Start.Startup>())
            {
                var request = new NewAppointmentCallRequest()
                {
                    Customer = new Model.Customer()
                    {
                        CustomerId = Guid.Empty,
                        Name = "Customer.Name",
                    }
                };

                var response = server.PostRequest("call/appointment", request);

                response.AssertIs(
                    HttpStatusCode.BadRequest,
                    new CommandError() { Message = "Invalid Command" });
            }
        }

        [TestMethod]
        public void PostCallAppointment_Empty_Appointment_AgentId_BadRequest()
        {
            using (var server = TestServer.Create<MoveManager.Web.Api.App_Start.Startup>())
            {
                var request = new NewAppointmentCallRequest()
                {
                    Customer = new Model.Customer()
                    {
                        CustomerId = Guid.Empty,
                        Name = "Customer.Name",
                    },
                    Appointment = new Model.Appointment()
                    {
                        AgentId = Guid.Empty
                    }
                };

                var response = server.PostRequest("call/appointment", request);

                response.AssertIs(
                    HttpStatusCode.Conflict,
                    new CommandError() { Message = "Invalid Command" });
            }
        }

        [TestMethod]
        public void PostCallAppointment_Empty_Appointment_Not_Existing_AgentId_BadRequest()
        {
            using (var server = TestServer.Create<MoveManager.Web.Api.App_Start.Startup>())
            {
                var request = new NewAppointmentCallRequest()
                {
                    Customer = new Model.Customer()
                    {
                        CustomerId = Guid.Empty,
                        Name = "Customer.Name",
                    },
                    Appointment = new Model.Appointment()
                    {
                        AgentId = Guid.NewGuid()
                    }
                };

                var response = server.PostRequest("call/appointment", request);

                response.AssertIs(
                    HttpStatusCode.BadRequest,
                    new CommandError() { Message = "Invalid Command" });
            }
        }

        [TestMethod]
        public void PostCallAppointment_Ambiguous_CustomerId_In_Appointment_BadRequest()
        {
            using (var server = TestServer.Create<MoveManager.Web.Api.App_Start.Startup>())
            {
                var request = new NewAppointmentCallRequest()
                {
                    Appointment = new Model.Appointment()
                    {
                        AgentId = WebApiHelper.Agent.Create(server),
                        CustomerId = Guid.Empty,
                    },
                    Customer = new Model.Customer()
                    {
                        CustomerId = Guid.NewGuid()
                    }
                };

                var response = server.PostRequest("call/appointment", request);

                response.AssertIs(
                    HttpStatusCode.BadRequest,
                    new CommandError() {Message = "AgentId cannot be null"});
            }
        }

        [TestMethod]
        public void PostCallAppointment_Ambiguous_CustomerId_In_CustomerBadRequest()
        {
            using (var server = TestServer.Create<MoveManager.Web.Api.App_Start.Startup>())
            {
                var request = new NewAppointmentCallRequest()
                {
                    Appointment = new Model.Appointment()
                    {
                        AgentId = WebApiHelper.Agent.Create(server),
                        CustomerId = Guid.NewGuid()
                    },
                    Customer = new Model.Customer()
                    {
                        CustomerId = Guid.Empty
                    }
                };

                var response = server.PostRequest("call/appointment", request);

                response.AssertIs(
                    HttpStatusCode.BadRequest,
                    new CommandError() { Message = "AgentId cannot be null" });
            }
        }
    }
}

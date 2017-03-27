using System;
using System.Collections.Generic;
using MoveManager.Business;
using MoveManager.Business.Agent;
using MoveManager.Business.Customer;
using MoveManager.Web.Api.Models;
using SimpleCqrs.Interface;

namespace MoveManager.Web.Api.Services
{
    public class CallService : ICallService
    {
        private readonly ICommandProcessor CommandProcessor;

        public CallService(ICommandProcessor commandProcessor)
        {
            this.CommandProcessor = commandProcessor;
        }

        public IEither<IEnumerable<ICommandError>, NewAppointmentResponse> NewAppointmentCall(
            NewAppointmentCallRequest newAppointmentCall)
        {
            return
                this.CreateOrUpdateCustomer(newAppointmentCall.Customer)
                    .Right(customer =>
                        this.CreateOrUpdateAppointment(customer.Model.CustomerId, newAppointmentCall.Appointment)
                            .Right(
                                agent => new NewAppointmentResponse
                                {
                                    customer = customer,
                                    agent = agent
                                }));
        }

        private IEither<IEnumerable<ICommandError>, Customer> CreateOrUpdateCustomer(Model.Customer customer)
        {
            return this.CommandProcessor.Process(
                new Business.Customer.Commands.CustomerUpdateCommand
                {
                    Data = customer
                });
        }

        private IEither<IEnumerable<ICommandError>, Agent> CreateOrUpdateAppointment(Guid customerId,
            Model.Appointment appointment)
        {
            appointment.CustomerId = customerId;

            return this.CommandProcessor.Process(
                new Business.Agent.Commands.AppointmentUpdateCommand
                {
                    Data = appointment
                });
        }
    }
}
using System;
using MoveManager.Business;
using MoveManager.Business.Agent;
using SimpleCqrs.Implementation.AggregateStorages.SqlServer;
using SimpleCqrs.Implementation.CommandHandlerInvokers;
using SimpleCqrs.Implementation.CommandHandlersLocators;
using SimpleCqrs.Implementation.CommandProcessors;

namespace MoveManager.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var command1 = new Commands.AgentUpdateCommand()
            {
                Data = new Model.Agent()
                {
                    AgentId = Guid.Empty,
                    Name = "Agente 01"
                }
            };

            var command2 = new Commands.AppointmentUpdateCommand()
            {
                Data = new Model.Appointment()
                {
                    AgentId = Guid.Parse("{66f14964-28b1-4b66-a182-9377d2957489}"),
                    CustomerId = Guid.Parse("{5E045EF2-39B2-4C79-B6D0-4F6E36AD00FD}"),

                    StartDate = DateTime.Parse("2017-03-05T12:30"),
                    EndDate = DateTime.Parse("2017-03-05T13:30"),
                    Address = new Model.Address()
                    {
                        Country = "España",
                        City = "Madrid",
                        Street = "Castellana 189"
                    }
                }
            };

            var command3 = new Commands.AgentUpdateCommand()
            {
                Data = new Model.Agent()
                {
                    AgentId = Guid.Parse("{66f14964-28b1-4b66-a182-9377d2957489}"),
                    Name = "Agente 02"
                }
            };

            var command4 = new Commands.AppointmentUpdateCommand()
            {
                Data = new Model.Appointment()
                {
                    AgentId = Guid.Parse("{66f14964-28b1-4b66-a182-9377d2957489}"),
                    CustomerId = Guid.Parse("{5E045EF2-39B2-4C79-B6D0-4F6E36AD00FD}"),

                    StartDate = DateTime.Parse("2017-03-05T13:30"),
                    EndDate = DateTime.Parse("2017-03-05T14:30"),
                    Address = new Model.Address()
                    {
                        Country = "España",
                        City = "Madrid",
                        Street = "Castellana 189"
                    }
                }
            };

            var cp = Ioc.ResolveCommandProcessor();

            cp.Process<AgentAggregateRoot, Commands.AgentUpdateCommand>(command1);
            cp.Process<AgentAggregateRoot, Commands.AppointmentUpdateCommand>(command2);
            cp.Process<AgentAggregateRoot, Commands.AgentUpdateCommand>(command3);
            cp.Process<AgentAggregateRoot, Commands.AppointmentUpdateCommand>(command4);
        }
    }
}

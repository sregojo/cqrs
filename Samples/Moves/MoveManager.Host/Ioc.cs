using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveManager.Business.Agent;
using SimpleCqrs.Implementation.AggregateStorages.SqlServer;
using SimpleCqrs.Implementation.CommandHandlerInvokers;
using SimpleCqrs.Implementation.CommandHandlersLocators;
using SimpleCqrs.Implementation.CommandProcessors;
using SimpleCqrs.Interface;

namespace MoveManager.Host
{
    public class Ioc
    {
        public static ICommandProcessor ResolveCommandProcessor()
        {
            return new CommandProcessor();
        }
    }

    class CommandProcessor : TransactionalCommandProcessor
    {
        public CommandProcessor() : base(
            new SqlAggregateStorage(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"),
            new InMemoryCommandHandlerLocator(),
            new SecuentialCommandHandlerInvoker())
        {
            InMemoryCommandHandlerLocator.Register<CommandHandler, AgentAggregateRoot, Commands.AgentUpdateCommand>();
            InMemoryCommandHandlerLocator.Register<CommandHandler, AgentAggregateRoot, Commands.AppointmentUpdateCommand>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoveManager.Business;
using MoveManager.Business.Agent;
using SimpleCqrs.Interface;

namespace MoveManager.Web.Api.Services
{
    public class AgentService : IAgentService
    {
        private readonly ICommandProcessor CommandProcessor;

        public AgentService(ICommandProcessor commandProcessor)
        {
            this.CommandProcessor = commandProcessor;
        }

        public IEither<IEnumerable<ICommandError>, Agent> CreateOrUpdateAgent(Model.Agent agent)
        {
            return this.CommandProcessor.Process(
                new Business.Agent.Commands.AgentUpdateCommand
                {
                    Data = agent
                });
        }
    }
}
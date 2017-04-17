using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveManager.Business;
using MoveManager.Business.Agent;
using MoveManager.Web.Api.Models;
using SimpleCqrs.Interface;

namespace MoveManager.Web.Api.Services
{
    public interface IAgentService
    {
        IEither<IEnumerable<ICommandError>, Agent> CreateOrUpdateAgent(Model.Agent agent);
    }
}

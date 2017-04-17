using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoveManager.Business;
using MoveManager.Web.Api.Services;

namespace MoveManager.Web.Api.Controllers
{
    public class AgentController : ApiController
    {
        private readonly IAgentService AgentService;

        public AgentController(IAgentService agentService)
        {
            this.AgentService = agentService;
        }

        [Route("agent")]
        [HttpPost]
        public IHttpActionResult PostAgent(Model.Agent agent)
        {
            try
            {
                return this.AgentService.CreateOrUpdateAgent(agent)
                    .Case<IHttpActionResult>(
                        errors => this.Conflict(),
                        newAgent => this.Ok(newAgent));
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }
    }
}

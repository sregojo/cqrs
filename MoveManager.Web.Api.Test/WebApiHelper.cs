using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using MoveManager.Business;

namespace MoveManager.Web.Api.Test
{
    public static class WebApiHelper
    {
        public static class Agent
        {
            public static Guid Create(TestServer server)
            {
                var response = server.PostRequest("agent", new Model.Agent()
                {
                    Name = "Agent.Test"
                });

                response.AssertIsOk();

                return response.Content<Business.Agent.Agent>().Id;
            }
        }
    }
}

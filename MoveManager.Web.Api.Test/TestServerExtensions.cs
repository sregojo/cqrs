using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;

namespace MoveManager.Web.Api.Test
{
    public static class TestServerExtensions
    {
        public static HttpResponseMessage PostRequest<T>(this TestServer server, string route, T request)
        {
            using (var client = server.HttpClient)
            {
                client.BaseAddress = new Uri("http://server.test");
                return client.PostAsJsonAsync(route, request).Result;
            }
        }
    }
}

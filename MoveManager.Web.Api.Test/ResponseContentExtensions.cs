using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace MoveManager.Web.Api.Test
{
    public static class ResponseContentExtensions
    {
        public static T Content<T>(this HttpResponseMessage response)
        {
            var x = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(x);
        }
    }
}

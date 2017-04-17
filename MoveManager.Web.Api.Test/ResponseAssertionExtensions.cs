using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoveManager.Web.Api.Test
{
    public static class ResponseAssertionExtensions
    {
        public static void AssertIs(this HttpResponseMessage response, HttpStatusCode statusCode, params CommandError[] errors)
        {
            Assert.AreEqual(response.StatusCode, statusCode);
        }

        public static void AssertIs<T>(this HttpResponseMessage response, HttpStatusCode code, T content)
        {
        }

        public static void AssertIsOk(this HttpResponseMessage response)
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}

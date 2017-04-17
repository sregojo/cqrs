using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCqrs.Interface;

namespace MoveManager.Web.Api.Test
{
    public class CommandError : ICommandError
    {
        public string Message { get; set; }
    }
}

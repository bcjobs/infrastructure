using Demo.Web.Services;
using Events;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Web.Test.Services
{
    public class GreetingHandler :
        IHandler<Greeting>,
        IHandler<If<Greeting, Succeeded>>,
        IHandler<If<Greeting, Failed>>
    {
        public async Task<bool> HandleAsync(If<Greeting, Failed> e)
        {
            return true;
        }

        public async Task<bool> HandleAsync(If<Greeting, Succeeded> e)
        {
            return true;
        }

        public async Task<bool> HandleAsync(Greeting e)
        {
            return true;
        }
    }
}

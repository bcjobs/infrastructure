using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Web.Test.Services
{
    class Greeter : 
        IHandler<Meeting>, 
        IHandler<If<Meeting, Failed>>, 
        IHandler<If<Greeting, Failed>>,
        IHandler<If<Meeting, Unhandled>>
    {
        public async Task<bool> HandleAsync(If<Meeting, Failed> e)
        {
            return true;
        }

        public async Task<bool> HandleAsync(If<Meeting, Unhandled> e)
        {
            return true;
        }

        public async Task<bool> HandleAsync(If<Greeting, Failed> e)
        {
            return true;
        }

        public async Task<bool> HandleAsync(Meeting e)
        {
            //throw new GoAwayException();
            await e.ReplyAsync(new Greeting());
            await e.ReplyAsync(new Greeting());
            return true;
        }
    }

    public class GoAwayException : Exception
    {
        public GoAwayException()
            : base("Go Away!")
        {
        }
    }
}

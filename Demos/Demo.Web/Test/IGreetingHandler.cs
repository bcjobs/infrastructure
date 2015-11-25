using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.Events;

namespace Demo.Web.Test
{
    public interface IGreetingHandler : 
        IHandler<Greeting>, 
        IHandler<If<Greeting, Succeeded>>, 
        IHandler<If<Greeting, Failed>>
    {
    }
}

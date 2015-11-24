using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Web.Test.Services
{
    public class Greeter : IGreeter
    {
        public string SayHello()
        {
            new Greeting("Hello World").Implement();
            //throw new NotImplementedException("Just a test");
            return "Hello World";
        }
    }
}

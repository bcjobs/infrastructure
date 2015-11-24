using Demo.Web.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Events;
using Demo.Web.Test.Services;
using Logs;
using System.Diagnostics.Contracts;
using System.Web;
using Authentications;
using System.Security;

namespace Demo.Web.Controllers
{    
    public class DefaultController : ApiController
    {
        public DefaultController(ILog log, IGreeter greeter)
        {
            Contract.Requires<ArgumentNullException>(log != null);
            Contract.Ensures(Log != null);
            Log = log;
            Greeter = greeter;
        }

        ILog Log { get; }
        IGreeter Greeter { get; }

        public IEnumerable<object> Get()
        {
            try
            {
                var hello = Greeter.SayHello();
            }
            catch
            {

            }
            return Log.Read(new LogQuery<Greeting>());
        }        
    }
}

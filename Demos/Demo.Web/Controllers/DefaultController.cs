using BookStore;
using Demo.Web.Test;
using Infra.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http;

namespace Demo.Web.Controllers
{
    public class DefaultController : ApiController
    {
        public DefaultController(ILog log, IGreeter greeter)
        {
            Contract.Requires<ArgumentNullException>(log != null);
            Contract.Requires<ArgumentNullException>(greeter != null);
            Contract.Ensures(Log != null);
            Contract.Ensures(Greeter != null);
            Log = log;
            Greeter = greeter;
        }

        ILog Log { get; }
        IGreeter Greeter { get; }

        public IEnumerable<object> Get()
        {
            return Log.Read(new LogQuery<PriceChanged>());
        }        
    }
}

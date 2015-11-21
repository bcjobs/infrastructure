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

namespace Demo.Web.Controllers
{    
    public class DefaultController : ApiController
    {
        public DefaultController(ILogWriter logWriter, ILogReader logReader)
        {
            Contract.Requires<ArgumentNullException>(logWriter != null);
            Contract.Requires<ArgumentNullException>(logReader != null);
            Contract.Ensures(LogWriter != null);
            Contract.Ensures(LogReader != null);
            LogWriter = logWriter;
            LogReader = logReader;
        }

        ILogWriter LogWriter { get; }
        ILogReader LogReader { get; }

        public async Task<string> Get()
        {
            LogWriter.WriteEvent(new Meeting());
            LogWriter.WriteEvent(new Greeting());

            var mm = LogReader.Read(new EventQuery<Meeting>());


            return "OK";
            //try
            //{
            //    var greeting = await new Meeting()
            //        .RequestAsync<Greeting>();

            //    return "OK"; // greeting.Text;
            //}
            //catch (GoAwayException ex)
            //{
            //    var m = ex.Message;
            //    return m;
            //}
            //catch (TooManyRepliesException ex)
            //{
            //    var m = ex.Message;
            //    return m;
            //}
            //catch (MissingReplyException ex)
            //{
            //    var m = ex.Message;
            //    return m;
            //}

        }
    }
}

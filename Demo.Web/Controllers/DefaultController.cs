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

namespace Demo.Web.Controllers
{
    public class DefaultController : ApiController
    {
        public async Task<string> Get()
        {
            try
            {
                var greeting = await new Meeting()
                    .RequestAsync<Greeting>();

                return "OK"; // greeting.Text;
            }
            catch (GoAwayException ex)
            {
                var m = ex.Message;
                return m;
            }
            catch (TooManyRepliesException ex)
            {
                var m = ex.Message;
                return m;
            }
            catch (MissingReplyException ex)
            {
                var m = ex.Message;
                return m;
            }

        }
    }
}

using Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Demo.Web.Services
{
    public class Authenticator : IAuthenticator
    {
        public string ApiKey => null;
        public IPAddress ClientIP => IPAddress.None;            
        public string ImpersonatorId => null;            
        public string UserId => null;
    }
}

using Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Events;

namespace Demo.Web.Services
{
    public class Authenticator : IAuthenticator
    {
        public string ApiKey => null;
        public IPAddress ClientIP => IPAddress.None;            
        public string ImpersonatorId => null;            
        public string UserId {
            get {
                return "Dima";
            }
        }
    }
}

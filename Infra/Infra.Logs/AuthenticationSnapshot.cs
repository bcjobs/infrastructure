using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Logs
{
    public class AuthenticationSnapshot
    {
        public AuthenticationSnapshot(string userId, string impersonatorId, string apiKey, IPAddress clientIP)
        {
            UserId = userId;
            ImpersonatorId = impersonatorId;
            ApiKey = apiKey;
            ClientIP = clientIP;
        }

        public string UserId { get; }
        public string ImpersonatorId { get; }
        public string ApiKey { get; }
        public IPAddress ClientIP { get; }
    }
}

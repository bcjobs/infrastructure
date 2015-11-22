using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Authentifications
{
    public interface IAuthenticator
    {
        string UserId { get; }
        string ImpersonatorId { get; }
        string ApiKey { get; }
        IPAddress ClientIP { get; }
    }

    public static class Authenticator
    {
        public static bool IsAuthenticated(this IAuthenticator athentificator)
        {
            return athentificator.UserId != null;
        }

        public static bool IsImpersonated(this IAuthenticator athentificator)
        {
            return athentificator.UserId != athentificator.ImpersonatorId;
        }
    }
}

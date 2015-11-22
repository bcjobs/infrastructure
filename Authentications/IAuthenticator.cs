using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Authentications
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
        public static void Authenticate(this IAuthenticator authenticator)
        {
            if (!authenticator.IsAuthenticated())
                throw new AuthenticationException();
        }

        public static bool IsAuthenticated(this IAuthenticator authenticator)
        {
            return authenticator.UserId != null;
        }

        public static bool IsImpersonated(this IAuthenticator authenticator)
        {
            return authenticator.IsAuthenticated() &&
                authenticator.UserId != authenticator.ImpersonatorId;
        }
    }
}

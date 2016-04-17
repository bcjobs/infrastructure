using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public interface IAuthenticator
    {
        string UserId { get; }
        string ImpersonatorId { get; }
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

        public static T UserId<T>(this IAuthenticator authenticator) {
            try
            {
                return authenticator.UserId.To<T>();
            }
            catch (InvalidCastException)
            {
                throw new AuthenticationException();
            }
        }

        public static T ImpersonatorId<T>(this IAuthenticator authenticator)
        {
            try
            {
                return authenticator.ImpersonatorId.To<T>();
            }
            catch (InvalidCastException)
            {
                throw new AuthenticationException();
            }
        }
    }
}

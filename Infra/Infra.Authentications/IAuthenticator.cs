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
    [ContractClass(typeof(AuthenticatorContract))]
    public interface IAuthenticator
    {
        string UserId { get; }
        string ImpersonatorId { get; }
        IPAddress ClientIP { get; }
    }

    [ContractClassFor(typeof(IAuthenticator))]
    abstract class AuthenticatorContract : IAuthenticator
    {
        public string ApiKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IPAddress ClientIP
        {
            get
            {
                Contract.Ensures(Contract.Result<IPAddress>() != null);
                throw new NotImplementedException();
            }
        }

        public string ImpersonatorId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string UserId
        {
            get
            {
                throw new NotImplementedException();
            }
        }
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

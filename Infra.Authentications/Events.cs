using Infra.Events;
using Infra.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public abstract class AuthenticationAction : ILoggable
    {        
    }

    public abstract class SessionAction : AuthenticationAction
    {
    }

    public class SessionStart : SessionAction
    {
        public SessionStart(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class SessionImpersonation : SessionAction
    {
        public SessionImpersonation(string userId, string impersonatorId)
        {
            UserId = userId;
            ImpersonatorId = impersonatorId;
        }

        public string UserId { get; }
        public string ImpersonatorId { get; }
    }

    public class SessionTermination : SessionAction
    {
        public SessionTermination(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }

    public abstract class PasswordAction : AuthenticationAction
    {
    }

    public class PasswordReset : PasswordAction
    {
        public PasswordReset(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class PasswordChange : PasswordAction
    {
        public PasswordChange(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class PasswordTokenCreation : PasswordAction
    {
        public PasswordTokenCreation(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class PasswordCreation : PasswordAction
    {
        public PasswordCreation(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class SignedInAsync
    {
        public SignedInAsync(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; }
    }
}

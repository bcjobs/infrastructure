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
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class SessionImpersonation : SessionAction
    {
        public SessionImpersonation(string userId, string impersonatorId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Requires<ArgumentNullException>(impersonatorId != null);
            Contract.Ensures(UserId != null);
            Contract.Ensures(ImpersonatorId != null);
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
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
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
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class PasswordChange : PasswordAction
    {
        public PasswordChange(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class PasswordTokenCreation : PasswordAction
    {
        public PasswordTokenCreation(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class PasswordCreation : PasswordAction
    {
        public PasswordCreation(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class UserActivity : AuthenticationAction
    {
        public UserActivity(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId == null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }
        public string UserId { get; }
    }
}

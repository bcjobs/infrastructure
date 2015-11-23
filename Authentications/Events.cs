using Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentifications
{
    public abstract class AuthentificationAction : ILoggable
    {
    }

    public abstract class SessionAction : AuthentificationAction
    {
    }

    public class SessionStarted : SessionAction
    {
        public SessionStarted(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class SessionImpersonated : SessionAction
    {
        public SessionImpersonated(string userId, string impersonatorId)
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

    public class SessionTerminated : SessionAction
    {
        public SessionTerminated(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }

    public abstract class PasswordAction : AuthentificationAction
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

    public class PasswordChanged : PasswordAction
    {
        public PasswordChanged(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class PasswordTokenCreated : PasswordAction
    {
        public PasswordTokenCreated(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class PasswordCreated : PasswordAction
    {
        public PasswordCreated(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(UserId != null);
            UserId = userId;
        }

        public string UserId { get; }
    }
}

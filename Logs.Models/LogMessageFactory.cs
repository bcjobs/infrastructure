using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs.Models
{
    public class LogMessageFactory
    {
        public LogMessageFactory(
            DateTime loggedAt,
            AuthentificationSnapshot authentificationSnapshot,
            object e,
            Exception ex)
        {
            Contract.Requires<ArgumentNullException>(authentificationSnapshot != null);
            Contract.Requires<ArgumentNullException>(e != null || ex != null);
            Contract.Ensures(authentificationSnapshot != null);
            Contract.Ensures(Event != null || Exception != null);
            LoggedAt = loggedAt;
            AuthentificationSnapshot = authentificationSnapshot;
            Event = e;
            Exception = ex;
        }

        DateTime LoggedAt { get; }
        AuthentificationSnapshot AuthentificationSnapshot { get; }
        object Event { get; }
        Exception Exception { get; }

        public object Create()
        {
            return Activator.CreateInstance(MessageType, 
                LoggedAt, AuthentificationSnapshot, Event, Exception);
        }

        Type MessageType => typeof(LogMessage<,>).MakeGenericType(
                Event?.GetType() ?? typeof(object),
                Exception?.GetType() ?? typeof(Exception));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    public class LogMessage<E, EX>
    {
        public LogMessage(
            DateTime loggedAt,
            AuthentificationSnapshot authentificationSnapshot,
            E e,
            EX ex)
        {
            Contract.Requires<ArgumentNullException>(authentificationSnapshot != null);
            Contract.Requires<ArgumentNullException>(e != null || ex != null);
            Contract.Ensures(AuthentificationSnapshot != null);
            Contract.Ensures(e != null || ex != null);
            LoggedAt = loggedAt;
            AuthentificationSnapshot = authentificationSnapshot;
            Event = e;
            Exception = ex;
        }

        public DateTime LoggedAt { get; }
        public AuthentificationSnapshot AuthentificationSnapshot { get; }
        public E Event { get; }
        public EX Exception { get; }
    }
}

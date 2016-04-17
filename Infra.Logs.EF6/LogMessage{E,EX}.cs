using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Logs.EF6
{
    public class LogMessage<E, EX> : ILogMessage<E,EX>
        where EX : Exception
    {
        public LogMessage(
            DateTime loggedAt,
            AuthenticationSnapshot authenticationSnapshot,
            E e,
            EX ex)
        {
            LoggedAt = loggedAt;
            AuthenticationSnapshot = authenticationSnapshot;
            Event = e;
            Exception = ex;
        }

        public DateTime LoggedAt { get; }
        public AuthenticationSnapshot AuthenticationSnapshot { get; }
        public E Event { get; }
        public EX Exception { get; }
    }
}

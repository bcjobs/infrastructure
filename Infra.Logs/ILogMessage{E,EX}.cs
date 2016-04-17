using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Logs
{
    public interface ILogMessage<out E, out EX>
        where EX : Exception
    {
        DateTime LoggedAt { get; }
        AuthenticationSnapshot AuthenticationSnapshot { get; }
        E Event { get; }
        EX Exception { get; }
    }
}

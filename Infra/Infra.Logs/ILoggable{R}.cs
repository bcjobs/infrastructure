using Infra.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Logs
{
    public interface ILoggable<R>
        where R : Result
    {
    }
}

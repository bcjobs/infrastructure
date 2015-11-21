using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    public interface ILoggable<R>
        where R : Result
    {
    }
}

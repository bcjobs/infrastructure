using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    public interface ILogReader
    {
        IEnumerable<LogMessage<E, EX>> Read<E, EX>(LogQuery<E, EX> query)
            where EX : Exception;
    }
}

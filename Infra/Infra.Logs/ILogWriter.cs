using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Logs
{
    [ContractClass(typeof(LogWriterContract))]
    public interface ILogWriter
    {
        void Write<E, EX>(E e, EX ex)
            where EX : Exception;
    }

    [ContractClassFor(typeof(ILogWriter))]
    abstract class LogWriterContract : ILogWriter
    {
        public void Write<E, EX>(E e, EX ex)            
            where EX : Exception
        {
            Contract.Requires<ArgumentNullException>(e != null || ex != null);
        }
    }

    public static class LogWriter
    {
        public static void WriteEvent<E>(this ILogWriter writer, E e)
        {
            writer.Write<E, Exception>(e, null);
        }

        public static void WriteException<EX>(this ILogWriter writer, EX ex)
            where EX : Exception
        {
            writer.Write<object, EX>(null, ex);
        }
    }
}

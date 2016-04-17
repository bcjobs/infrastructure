using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Logs
{
    public interface ILogWriter
    {
        void Write<E, EX>(E e, EX ex)
            where EX : Exception;
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

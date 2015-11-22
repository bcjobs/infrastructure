using Base;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    public class LogQuery : LogQuery<object>
    {
    }

    public class LogQuery<E> : LogQuery<E, Exception>
    {
    }

    public class LogQuery<E, EX> : TimeQuery     
        where EX : Exception
    {
        public LoggedData Event { get; set; } = LoggedData.Optional;
        public LoggedData Exception { get; set; } = LoggedData.Optional;
    }

    public enum LoggedData
    {
        Empty,
        Optional,
        Required
    }
}

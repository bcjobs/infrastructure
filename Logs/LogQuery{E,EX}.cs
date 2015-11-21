using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    public abstract class LogQuery<E, EX> : PaginatedQuery
        where EX : Exception
    {
        public abstract bool EventRequired { get; }
        public abstract bool ExceptionRequired { get; }
    }

    public class LogQuery : LogQuery<object, Exception>
    {
        public override bool EventRequired { get; } = false;
        public override bool ExceptionRequired { get; } = false;
    }

    public class ExceptionQuery<E, EX> : LogQuery<E, EX>
        where EX : Exception
    {
        public override bool EventRequired { get; } = false;
        public override bool ExceptionRequired { get; } = true;
    }

    public class ExceptionQuery<EX> : ExceptionQuery<object, EX>
        where EX : Exception
    {
    }

    public class ExceptionQuery : ExceptionQuery<Exception>        
    {
    }

    public class EventQuery<E, EX> : LogQuery<E, EX>
        where EX : Exception
    {
        public override bool EventRequired { get; } = true;
        public override bool ExceptionRequired { get; } = false;
    }

    public class EventQuery<E> : EventQuery<E, Exception>        
    {
    }

    public class EventQuery : EventQuery<object>
    {
    }
}

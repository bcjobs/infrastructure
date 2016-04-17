using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Events
{   
    public interface If<out T, R>
        where R : Result
    {
        T Subject { get; }
        Exception Exception { get; }
    }

    class Notification
    {
        public static If<object, R> Create<R>(object e, Exception ex = null)
            where R : Result
        {
            var eType = e?.GetType() ?? typeof(object);
            var rType = typeof(R);
            var type = typeof(Notification<,>).MakeGenericType(eType, rType);
            return (If<object, R>)Activator.CreateInstance(type, e, ex);
        }
    }

    class Notification<T, R> : If<T, R>
        where R : Result
    {
        public Notification(T subject, Exception exception = null)
        {
            Subject = subject;
            Exception = exception;
        }

        public T Subject { get; }
        public Exception Exception { get; }
    }
}

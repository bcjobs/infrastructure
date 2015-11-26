using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Events
{   
    [ContractClass(typeof(IfContract<,>))]
    public interface If<out T, R>
        where R : Result
    {
        T Subject { get; }
        Exception Exception { get; }
    }

    [ContractClassFor(typeof(If<,>))]
    abstract class IfContract<T, R> : If<T, R>
        where R : Result
    {
        public Exception Exception
        {
            get
            {                
                throw new NotImplementedException();
            }
        }

        public T Subject
        {
            get
            {
                //Contract.Ensures(Contract.Result<T>() != null);
                throw new NotImplementedException();
            }
        }
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
            Contract.Requires<ArgumentNullException>(subject != null || exception != null);
            Contract.Ensures(Subject != null || exception != null);
            Subject = subject;
            Exception = exception;
        }

        public T Subject { get; }
        public Exception Exception { get; }
    }
}

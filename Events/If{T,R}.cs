using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
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
                Contract.Ensures(Contract.Result<T>() != null);
                throw new NotImplementedException();
            }
        }
    }

    class Notification<T, R> : If<T, R>
        where R : Result
    {
        public Notification(T subject, Exception exception = null)
        {
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Ensures(Subject != null);
            Subject = subject;
            Exception = exception;
        }

        public T Subject { get; }
        public Exception Exception { get; }
    }
}

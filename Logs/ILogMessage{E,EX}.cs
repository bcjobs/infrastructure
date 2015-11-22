using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    [ContractClass(typeof(LogMessageContract<,>))]
    public interface ILogMessage<out E, out EX>
        where EX : Exception
    {
        DateTime LoggedAt { get; }
        AuthentificationSnapshot AuthentificationSnapshot { get; }
        E Event { get; }
        EX Exception { get; }
    }

    [ContractClassFor(typeof(ILogMessage<,>))]
    abstract class LogMessageContract<E, EX> : ILogMessage<E, EX>
        where EX : Exception
    {
        public AuthentificationSnapshot AuthentificationSnapshot
        {
            get
            {
                Contract.Ensures(Contract.Result<AuthentificationSnapshot>() != null);
                throw new NotImplementedException();
            }
        }

        public E Event
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public EX Exception
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DateTime LoggedAt
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    [ContractClass(typeof(WeakHandlerContract<>))]
    public interface IWeakHandler<in T>
    {
        Task<bool> HandleAsync(T e);
    }

    [ContractClassFor(typeof(IWeakHandler<>))]
    abstract class WeakHandlerContract<T> : IWeakHandler<T>
    {
        public Task<bool> HandleAsync(T e)
        {
            Contract.Requires<ArgumentNullException>(e != null);
            Contract.Ensures(Contract.Result<Task<bool>>() != null);
            throw new NotImplementedException();
        }
    }

    class WeakHandler<T> : IWeakHandler<T>
    {
        public WeakHandler(Func<T, Task<bool>> action)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            Contract.Ensures(Action != null);
            Action = action;
        }

        Func<T, Task<bool>> Action { get; }

        public Task<bool> HandleAsync(T e)
        {
            return Action(e);
        }
    }
}

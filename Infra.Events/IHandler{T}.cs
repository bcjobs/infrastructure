using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Events
{
    [ContractClass(typeof(HandlerContract<>))]
    public interface IHandler<in T>
    {
        Task<bool> HandleAsync(T e);
    }

    [ContractClassFor(typeof(IHandler<>))]
    abstract class HandlerContract<T> : IHandler<T>
    {
        public Task<bool> HandleAsync(T e)
        {
            Contract.Requires<ArgumentNullException>(e != null);
            Contract.Ensures(Contract.Result<Task<bool>>() != null);
            throw new NotImplementedException();
        }
    }

    class Handler<T> : IHandler<T>
    {
        public Handler(Func<T, Task<bool>> action)
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

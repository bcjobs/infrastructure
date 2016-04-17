using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Events
{
    public interface IWeakHandler<in T>
    {
        Task<bool> HandleAsync(T e);
    }

    class WeakHandler<T> : IWeakHandler<T>
    {
        public WeakHandler(Func<T, Task<bool>> action)
        {
            Action = action;
        }

        Func<T, Task<bool>> Action { get; }

        public Task<bool> HandleAsync(T e)
        {
            return Action(e);
        }
    }
}

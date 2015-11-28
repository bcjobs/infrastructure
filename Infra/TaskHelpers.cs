using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks
{
    public static class TaskHelpers
    {
        public static void Forget(this Task task)
        {
            task.ContinueWith(t =>
            {
                Debugger.Break();
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}

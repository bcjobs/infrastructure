using Mixins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    [Mixin]
    public interface ILog : ILogReader, ILogWriter
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Logs
{
    public interface ICustomLoggable        
    {
        object Event { get; }
        Exception Exception { get; }
    }
}

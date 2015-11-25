using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Services
{
    public class SystemClock : IClock
    {
        public DateTime GetTime()
        {
            return DateTime.UtcNow;
        }
    }
}

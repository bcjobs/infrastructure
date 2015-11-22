using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class TimeQuery : PaginatedQuery
    {
        public DateTime After { get; set; } = DateTime.MinValue;
        public DateTime Before { get; set; } = DateTime.MaxValue;
    }
}

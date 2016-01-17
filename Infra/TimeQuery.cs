﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class TimeQuery : PagingQuery
    {
        public DateTime After { get; set; } = DateTime.MinValue;
        public DateTime Before { get; set; } = DateTime.MaxValue;
    }
}

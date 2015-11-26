using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Logs.EF6.Entities
{
    class ELogMessage
    {
        public int Id { get; private set; }
        public DateTime LoggedAt { get; set; }

        public string UserId { get; set; }
        public string ImpersonatorId { get; set; }
        public string ClientIP { get; set; }

        public string EventJson { get; set; }
        public virtual IList<ELogType> EventTypes { get; set; }

        public string ExceptionJson { get; set; }
        public virtual IList<ELogType> ExceptionTypes { get; set; }
    }        
}

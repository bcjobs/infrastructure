using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logs.EF6.Services
{
    public class LogReader : ILogReader
    {
        public IEnumerable<ILogMessage<E, EX>> Read<E, EX>(LogQuery<E, EX> query) where EX : Exception
        {
            var messages = from m in new LogContext().Messages.AsNoTracking()
                           let eIsNull = m.EventJson == null
                           let exIsNull = m.ExceptionJson == null
                           let eMatches = m.EventTypes.Any(t => t.Name == typeof(E).FullName)
                           let exMatches = m.ExceptionTypes.Any(t => t.Name == typeof(EX).FullName)
                           where
                                (query.Event == LoggedData.Empty && eIsNull) ||
                                (query.Event == LoggedData.Optional && (eIsNull || eMatches)) ||
                                (query.Event == LoggedData.Required && eMatches)
                           where
                                (query.Exception == LoggedData.Empty && exIsNull) ||
                                (query.Exception == LoggedData.Optional && (exIsNull || exMatches)) ||
                                (query.Exception == LoggedData.Required && exMatches)
                           orderby m.LoggedAt descending
                           select m;

            return messages
                .Skip((query.Page - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .AsEnumerable()
                    .Select(m => new LogMessage<E, EX>(
                            m.LoggedAt,
                            new AuthentificationSnapshot(
                                m.UserId,
                                m.ImpersonatorId,
                                m.ApiKey,
                                IPAddress.Parse(m.ClientIP)),
                            m.EventJson.ToObject<E>(),
                            m.ExceptionJson.ToObject<EX>()
                        ));
        }        
    }
}

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
        public IEnumerable<LogMessage<E, EX>> Read<E, EX>(LogQuery<E, EX> query) where EX : Exception
        {
            var ctx = new LogContext();
            return ctx.Messages
                    .AsNoTracking()
                    .Where(m => !query.EventRequired || m.EventTypes.Any(t => t.Name == typeof(E).FullName))
                    .Where(m => !query.ExceptionRequired || m.ExceptionTypes.Any(t => t.Name == typeof(EX).FullName))
                    .OrderByDescending(m => m.LoggedAt)
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
                            m.EventJson == null ? default(E) : JsonConvert.DeserializeObject<E>(m.EventJson, JsonSettings),
                            m.ExceptionJson == null ? default(EX) : JsonConvert.DeserializeObject<EX>(m.ExceptionJson, JsonSettings)
                        ));
        }

        JsonSerializerSettings JsonSettings =>
            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    }
}

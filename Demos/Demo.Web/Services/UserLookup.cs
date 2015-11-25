using Infra.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Infra.Authentications;

namespace Demo.Web.Services
{
    public class UserLookup : IUserLookup
    {
        public string UserId(MailAddress email)
        {
            return Guid.Empty.ToString();
        }
    }
}

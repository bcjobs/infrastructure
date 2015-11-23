using Authentifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Authentications.Identity.Services
{
    public class Sessions : ISessions
    {
        public Task ImpersonateAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(MailAddress email, string password)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}

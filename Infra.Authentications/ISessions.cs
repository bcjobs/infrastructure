using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public interface ISessions
    {
        void SignIn(string userId);
        Task SignInAsync(string userId);
        Task SignInAsync(string email, string password);
        void Impersonate(string userId, string impersonatorId);
        Task ImpersonateAsync(string userId, string impersonatorId);
        Task SignOutAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{

    public interface IUserLookup
    {
        string UserId(string email);
    }

    public static class UserLookup
    {
        public static bool Exists(this IUserLookup userLookup, string email)
        {
            return userLookup.UserId(email) != null;
        }
    }
}

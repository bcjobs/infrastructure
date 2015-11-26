using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{

    [ContractClass(typeof(UserLookupContract))]
    public interface IUserLookup
    {
        string UserId(string email);
    }

    [ContractClassFor(typeof(IUserLookup))]
    abstract class UserLookupContract : IUserLookup
    {
        public string UserId(string email)
        {
            Contract.Requires<ArgumentNullException>(email != null);            
            throw new NotImplementedException();
        }
    }

    public static class UserLookup
    {
        public static bool Exists(this IUserLookup userLookup, string email)
        {
            Contract.Requires<ArgumentNullException>(userLookup != null);            
            Contract.Requires<ArgumentNullException>(email != null);
            return userLookup.UserId(email) != null;
        }
    }
}

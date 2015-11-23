using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentications.Identity
{
    static class AuthenticationManager
    {
        public static AuthenticationUser GetOrCreate(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(Contract.Result<AuthenticationUser>() != null);

            throw new NotImplementedException();
        }
    }
}

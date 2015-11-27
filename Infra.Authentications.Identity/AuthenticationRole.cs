using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications.Identity
{
    public class AuthenticationRole : IdentityRole
    {
        public AuthenticationRole() : base()
        {
        }

        public AuthenticationRole(string roleName) : base(roleName)
        {
        }
    }
}

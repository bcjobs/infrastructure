using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications.Identity
{
    public class AuthenticationRoleManager : RoleManager<AuthenticationRole>
    {
        public AuthenticationRoleManager(RoleStore<AuthenticationRole> store)
                : base(store)
        {
        }

        public static AuthenticationRoleManager Create(IdentityFactoryOptions<AuthenticationRoleManager> options,
            IOwinContext context)
        {
            return new AuthenticationRoleManager(new RoleStore<AuthenticationRole>(context.Get<AuthenticationContext>()));
        }
    }
}

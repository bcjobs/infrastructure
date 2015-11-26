using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications.Identity
{

    public class AuthenticationSignInManager : SignInManager<AuthenticationUser, string>
    {
        public AuthenticationSignInManager(AuthenticationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AuthenticationUser user)
        {
            return user.GenerateUserIdentityAsync((AuthenticationUserManager)UserManager);
        }

        public static AuthenticationSignInManager Create(IdentityFactoryOptions<AuthenticationSignInManager> options, IOwinContext context)
        {
            return new AuthenticationSignInManager(context.GetUserManager<AuthenticationUserManager>(), context.Authentication);
        }
    }
}

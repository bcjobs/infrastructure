using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infra.Authentications.Identity
{
    public static class IdentityManagers
    {

        public static AuthenticationUser GetOrCreate(string userId)
        {
            var user = UserManager.FindById(userId);
            if (user == null)
            {
                user = new AuthenticationUser
                {
                    Id = userId,
                    UserName = userId
                };

                var result = UserManager.Create(user);
                if (!result.Succeeded)
                    throw new InvalidOperationException(string.Join(", ", result.Errors));

            }

            return user;
        }

        public static async Task<AuthenticationUser> GetOrCreateAsync(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                user = new AuthenticationUser
                {
                    Id = userId,
                    UserName = userId
                };

                var result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                    throw new InvalidOperationException(string.Join(", ", result.Errors));

            }

            return user;
        }

        public static IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        public static AuthenticationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<AuthenticationUserManager>();
            }
        }

        public static AuthenticationSignInManager SignInManager
        {
            get
            {
                return Request.GetOwinContext().Get<AuthenticationSignInManager>();
            }
        }

        public static AuthenticationRoleManager RoleManager
        {
            get
            {
                return Request.GetOwinContext().Get<AuthenticationRoleManager>();
            }
        }

        static HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
    }
}

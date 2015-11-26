using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Infra.Authentications.Identity
{
    public class AuthenticationUserManager : UserManager<AuthenticationUser>
    {
        public AuthenticationUserManager(IUserStore<AuthenticationUser> store)
            : base(store)
        {
        }

        public static AuthenticationUserManager Create(IdentityFactoryOptions<AuthenticationUserManager> options, IOwinContext context)
        {
            var db = context.Get<AuthenticationContext>();
            var manager = new AuthenticationUserManager(new UserStore<AuthenticationUser>(db));
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
            manager.UserValidator = new UserValidator<AuthenticationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<AuthenticationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}
using Infra.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace Infra.Authentications.Identity.Services
{
    public class Sessions : ISessions
    {
        public Sessions(IUserLookup userLookup)
        {
            UserLookup = userLookup;
        }

        IUserLookup UserLookup { get; }

        public async Task ImpersonateAsync(string userId, string impersonatorId)
        {
            var user = await IdentityManagers.GetOrCreateAsync(userId);
            await SignInUserAsync(user, impersonatorId);
        }

        public void SignIn(string userId)
        {
            var user = IdentityManagers.GetOrCreate(userId);
            SignInUser(user);
        }

        public async Task SignInAsync(string userId)
        {
            var user = await IdentityManagers.GetOrCreateAsync(userId);
            await SignInUserAsync(user);
        }

        public async Task SignInAsync(string email, string password)
        {
            var userId = UserLookup.UserId(email);
            if (userId == null)
                throw new InvalidCredentialsException();

            var user = await IdentityManagers.UserManager.FindAsync(userId.ToString(), password);
            if (user == null)
                throw new InvalidCredentialsException();

            await SignInUserAsync(user);
        }

        void SignInUser(AuthenticationUser user, string impersonatorId = null)
        {
            var identity = IdentityManagers.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            if (impersonatorId != null)
                identity.AddImpersonatorId(impersonatorId);

            IdentityManagers.AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, identity);

            System.Threading.Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
        }

        async Task SignInUserAsync(AuthenticationUser user, string impersonatorId = null)
        {
            var identity = await IdentityManagers.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            if (impersonatorId != null)
                identity.AddImpersonatorId(impersonatorId);

            IdentityManagers.AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, identity);

            System.Threading.Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
        }

        public Task SignOutAsync()
        {
            IdentityManagers.AuthenticationManager.SignOut();
            return Task.CompletedTask;
        }
    }
}

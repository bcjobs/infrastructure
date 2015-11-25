using Infra.Events;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications.Identity.Services
{
    public class OwinConfigurator : IHandler<IAppBuilder>
    {
        public Task<bool> HandleAsync(IAppBuilder app)
        {
            app.CreatePerOwinContext(AuthenticationContext.Create);
            app.CreatePerOwinContext<AuthenticationUserManager>(AuthenticationUserManager.Create);
            app.CreatePerOwinContext<AuthenticationSignInManager>(AuthenticationSignInManager.Create);

            var cookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            };

            if (ConfigurationManager.AppSettings["security:LoginPath"] != null)
                cookieOptions.LoginPath = new PathString(ConfigurationManager.AppSettings["security:LoginPath"]);

            if (ConfigurationManager.AppSettings["security:CookieName"] != null)
                cookieOptions.CookieName = ConfigurationManager.AppSettings["security:CookieName"];

            if (ConfigurationManager.AppSettings["security:CookieDomain"] != null)
                cookieOptions.CookieDomain = ConfigurationManager.AppSettings["security:CookieDomain"];

            app.UseCookieAuthentication(cookieOptions);

            return Task.FromResult(true);
        }
    }
}

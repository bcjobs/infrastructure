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

namespace Infra.Authentications.Identity.Services.Transient
{
    public class OwinConfigurator : IHandler<IAppBuilder>
    {
        public Task<bool> HandleAsync(IAppBuilder app)
        {
            app.CreatePerOwinContext(AuthenticationContext.Create);
            app.CreatePerOwinContext<AuthenticationUserManager>(AuthenticationUserManager.Create);
            app.CreatePerOwinContext<AuthenticationSignInManager>(AuthenticationSignInManager.Create);
            app.CreatePerOwinContext<AuthenticationRoleManager>(AuthenticationRoleManager.Create);

            var cookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            };

            if (ConfigurationManager.AppSettings["authentications:LoginPath"] != null)
                cookieOptions.LoginPath = new PathString(ConfigurationManager.AppSettings["authentications:LoginPath"]);

            // http://stackoverflow.com/a/20151056/188740
            if (ConfigurationManager.AppSettings["authentications:IgnoreLoginPathIfRequestUrlStartsWithSegment"] != null)
            {
                cookieOptions.Provider = new CookieAuthenticationProvider
                {
                    OnApplyRedirect = context =>
                    {
                        if (!context.Request.Path.StartsWithSegments(new PathString(ConfigurationManager.AppSettings["authentications:IgnoreLoginPathIfRequestUrlStartsWithSegment"])))
                            context.Response.Redirect(context.RedirectUri);
                    }
                };
            }

            if (ConfigurationManager.AppSettings["authentications:CookieName"] != null)
                cookieOptions.CookieName = ConfigurationManager.AppSettings["authentications:CookieName"];

            if (ConfigurationManager.AppSettings["authentications:CookieDomain"] != null)
                cookieOptions.CookieDomain = ConfigurationManager.AppSettings["authentications:CookieDomain"];

            if (ConfigurationManager.AppSettings["authentications:CookieSecure"] != null)
                cookieOptions.CookieSecure = (CookieSecureOption)Enum.Parse(typeof(CookieSecureOption), ConfigurationManager.AppSettings["authentications:CookieSecure"], true);

            app.UseCookieAuthentication(cookieOptions);

            return Task.FromResult(true);
        }
    }
}

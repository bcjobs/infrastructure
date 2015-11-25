using Microsoft.AspNet.Identity;
using Owin;
using Infra.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Web.App_Start
{
    public class OwinConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.RaiseAsync().Wait();

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            //app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            //{
            //    AppId = "",
            //    AppSecret = "",
            //    Provider = new FacebookAuthProvider()
            //});
        }
    }
}
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Infra.Authentications.Identity.Migrations;

namespace Infra.Authentications.Identity
{
    class AuthenticationContext : IdentityDbContext<AuthenticationUser>
    {
        static AuthenticationContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthenticationContext, Configuration>());
        }

        public AuthenticationContext() :  base("Name=AuthenticationsIdentity") { }

        public static AuthenticationContext Create()
        {
            return new AuthenticationContext();
        }
    }
}

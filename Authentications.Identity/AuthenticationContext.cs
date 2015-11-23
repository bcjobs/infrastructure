using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Authentications.Identity.Migrations;

namespace Authentications.Identity
{
    class AuthenticationContext : IdentityDbContext<AuthenticationUser>
    {
        static AuthenticationContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthenticationContext, Configuration>());
        }

        public AuthenticationContext() :  base("Name=AuthenticationIdentity") { }

        public static AuthenticationContext Create()
        {
            return new AuthenticationContext();
        }
    }
}

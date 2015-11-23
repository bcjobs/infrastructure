using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Authentications.Identity
{
    class AuthenticationContext : IdentityDbContext<AuthenticationUser>
    {
        public AuthenticationContext() :  base("Name=AuthenticationIdentity") { }

        static AuthenticationContext()
        {
            Database.SetInitializer<AuthenticationContext>(new NullDatabaseInitializer<AuthenticationContext>());
        }

        public static AuthenticationContext Create()
        {
            return new AuthenticationContext();
        }
    }
}

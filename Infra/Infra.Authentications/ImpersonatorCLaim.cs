using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public static class ImpersonatorClaim
    {
        const string Type = "ImpersonatorId";

        public static string GetImpersonatorId(this ClaimsIdentity identity)
        {
            return identity.FindFirst(Type)?.Value ?? identity.Name;
        }

        public static void AddImpersonatorId(this ClaimsIdentity identity, string id)
        {
            identity.AddClaim(new Claim(Type, id));
        }
    }
}

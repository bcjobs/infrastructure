using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public class AuthenticationException : SecurityException
    {
        public AuthenticationException() 
            : base("Attempt of non authenticated access.")
        {
        }
    }
}

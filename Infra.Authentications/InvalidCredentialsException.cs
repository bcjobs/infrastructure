using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
            : base()
        {
        }

        public InvalidCredentialsException(string message)
            : base(message)
        {
        }
    }
}

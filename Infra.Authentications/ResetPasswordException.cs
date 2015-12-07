using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public class ResetPasswordException : Exception
    {
        public ResetPasswordException()
            : base()
        {
        }

        public ResetPasswordException(string message)
            : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public interface IRoles
    {
        Task CreateAsync(string name);
    }
}

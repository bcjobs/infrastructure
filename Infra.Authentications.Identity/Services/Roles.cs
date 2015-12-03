using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications.Identity.Services
{
    public class Roles : IRoles
    {
        public async Task CreateAsync(string name)
        {
            var role = new AuthenticationRole(name);
            var result = await IdentityManagers.RoleManager.CreateAsync(role);

            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors));
        }
    }
}

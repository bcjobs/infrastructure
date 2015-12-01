using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications.Identity.Services
{
    public class Users : IUsers
    {
        public async Task AddToRoleAsync(string userId, string role)
        {
            await IdentityManagers.UserManager.AddToRoleAsync(userId, role);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(string userId)
        {
            return await IdentityManagers.UserManager.GetRolesAsync(userId);
        }

        public async Task RemoveFromRoleAsync(string userId, string role)
        {
            await IdentityManagers.UserManager.RemoveFromRoleAsync(userId, role);
        }
    }
}

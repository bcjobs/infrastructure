using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications.Identity.Services
{
    public class Users : IUsers
    {
        public async Task CreateAsync(string userId, string password = null)
        {
            var appUser = new AuthenticationUser
            {
                Id = userId,
                UserName = userId
            };

            var result = password != null 
                ? await IdentityManagers.UserManager.CreateAsync(appUser, password)
                : await IdentityManagers.UserManager.CreateAsync(appUser);

            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors));
        }

        public async Task DeleteAsync(string userId)
        {
            var appUser = await IdentityManagers.UserManager.FindByIdAsync(userId);
            if (appUser == null)
                throw new InvalidOperationException("User requested for deleting couldn't be found.");

            var result = await IdentityManagers.UserManager.DeleteAsync(appUser);

            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors));
        }

        public async Task AddToRoleAsync(string userId, string role)
        {
            var result = await IdentityManagers.UserManager.AddToRoleAsync(userId, role);

            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors));
        }

        public async Task<IEnumerable<string>> GetRolesAsync(string userId)
        {
            return await IdentityManagers.UserManager.GetRolesAsync(userId);
        }

        public async Task RemoveFromRoleAsync(string userId, string role)
        {
            var result = await IdentityManagers.UserManager.RemoveFromRoleAsync(userId, role);

            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors));
        }
    }
}

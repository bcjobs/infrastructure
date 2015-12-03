using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public interface IUsers
    {
        Task CreateAsync(string userId, string password = null);
        Task DeleteAsync(string userId);
        Task AddToRoleAsync(string userId, string role);
        Task RemoveFromRoleAsync(string userId, string role);
        Task<IEnumerable<string>> GetRolesAsync(string userId);
    }
}

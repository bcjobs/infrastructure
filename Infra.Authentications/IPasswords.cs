using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    public interface IPasswords
    {
        Task ResetAsync(string userId, string token, string password);
        Task ChangeAsync(string userId, string currentPassword, string newPassword);
        Task<string> CreateTokenAsync(string userId);
        Task<string> CreateAsync(string userId);
    }
}

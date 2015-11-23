using Authentifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentications.Identity.Services
{
    public class Passwords : IPasswords
    {
        public Task ChangeAsync(string userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTokenAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task ResetAsync(string userId, string token, string password)
        {
            throw new NotImplementedException();
        }
    }
}

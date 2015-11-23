using Authentifications;
using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events;

namespace Authentications.Identity.Services
{
    public class Passwords : IPasswords
    {
        public async Task ChangeAsync(string userId, string currentPassword, string newPassword)
        {
            using (var e = new PasswordChange(userId).Start())
                try
                {
                    var result = await IdentityManagers.UserManager.ChangePasswordAsync(userId, currentPassword, newPassword);
                    if (!result.Succeeded)
                        throw new InvalidOperationException(string.Join(", ", result.Errors));                    
                }
                catch (Exception ex)
                {
                    e.Fail(ex);
                    throw;
                }
        }

        public async Task<string> CreateAsync(string userId)
        {
            using (var e = new PasswordCreation(userId).Start())
                try
                {
                    var user = new AuthenticationUser
                    {
                        Id = userId,
                        UserName = userId
                    };

                    string password = new RandomString(8);
                    var result = await IdentityManagers.UserManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                        throw new InvalidOperationException(string.Join(", ", result.Errors));

                    await e.RaiseAsync();
                    return password;
                }
                catch (Exception ex)
                {
                    e.Fail(ex);
                    throw;
                }
        }

        public async Task<string> CreateTokenAsync(string userId)
        {
            using (var e = new PasswordTokenCreation(userId).Start())
                try
                {
                    var user = await IdentityManagers.GetOrCreateAsync(userId);
                    var token = await IdentityManagers.UserManager.GeneratePasswordResetTokenAsync(userId);
                    return token;
                }
                catch (Exception ex)
                {
                    e.Fail(ex);
                    throw;
                }
        }

        public async Task ResetAsync(string userId, string token, string password)
        {
            using (var e = new PasswordReset(userId).Start())
                try
                {
                    var result = await IdentityManagers.UserManager.ResetPasswordAsync(userId, token, password);
                    if (!result.Succeeded)
                        throw new InvalidOperationException(string.Join(", ", result.Errors));
                }
                catch (Exception ex)
                {
                    e.Fail(ex);
                    throw;
                }
        }
    }
}

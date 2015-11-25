using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    [ContractClass(typeof(PasswordsContract))]
    public interface IPasswords
    {
        Task ResetAsync(string userId, string token, string password);
        Task ChangeAsync(string userId, string currentPassword, string newPassword);
        Task<string> CreateTokenAsync(string userId);
        Task<string> CreateAsync(string userId);
    }

    [ContractClassFor(typeof(IPasswords))]
    abstract class PasswordsContract : IPasswords
    {
        public static object Requires { get; internal set; }

        public Task ResetAsync(string userId, string token, string password)
        {
            Contract.Ensures(Contract.Result<Task>() != null);
            throw new NotImplementedException();
        }

        public Task ChangeAsync(string userId, string currentPassword, string newPassword)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Requires<ArgumentNullException>(currentPassword != null);
            Contract.Requires<ArgumentNullException>(newPassword != null);
            Contract.Ensures(Contract.Result<Task>() != null);
            throw new NotImplementedException();
        }

        public Task<string> CreateAsync(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(Contract.Result<Task<string>>() != null);
            throw new NotImplementedException();
        }

        public Task<string> CreateTokenAsync(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(Contract.Result<Task<string>>() != null);
            throw new NotImplementedException();
        }
    }
}

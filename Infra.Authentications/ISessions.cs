using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Authentications
{
    [ContractClass(typeof(SessionsContract))]
    public interface ISessions
    {
        Task SignInAsync(string userId);
        Task SignInAsync(string email, string password);
        Task ImpersonateAsync(string userId, string impersonatorId);
        Task SignOutAsync();
    }

    [ContractClassFor(typeof(ISessions))]
    abstract class SessionsContract : ISessions
    {
        public Task SignInAsync(string userId)
        {
            Contract.Requires<InvalidCredentialsException>(userId != null);
            Contract.Ensures(Contract.Result<Task>() != null);
            throw new NotImplementedException();
        }

        public Task SignInAsync(string email, string password)
        {
            Contract.Requires<InvalidCredentialsException>(email != null);
            Contract.Requires<InvalidCredentialsException>(password != null);
            Contract.Ensures(Contract.Result<Task>() != null);
            throw new NotImplementedException();
        }

        public Task ImpersonateAsync(string userId, string impersonatorId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Ensures(Contract.Result<Task>() != null);
            throw new NotImplementedException();
        }

        public Task SignOutAsync()
        {
            Contract.Ensures(Contract.Result<Task>() != null);
            throw new NotImplementedException();
        }
    }

}

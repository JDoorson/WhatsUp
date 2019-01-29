using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhatsUpV2.Contexts;
using WhatsUpV2.EFModels;
using WhatsUpV2.Interfaces;

namespace WhatsUpV2.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly WhatsUpContext ctx = new WhatsUpContext();

        /// <summary>
        ///     Attempt to log in the user with the provided credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Account> LogIn(string username, string password)
        {
            // Retrieve first account with given username
            var account = await ctx.Accounts.SingleOrDefaultAsync(acc => acc.Username == username);

            // If an account was found, check the password. Short-circuiting prevents errors.
            if (account != null && account.VerifyPassword(password))
            {
                return account;
            }

            // Account null or password incorrect
            return null;
        }

        /// <summary>
        ///     Register a user
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Task Register(Account account)
        {
            account.HashPassword();
            ctx.Accounts.Add(account);
            return ctx.SaveChangesAsync();
        }

        /// <summary>
        ///     Check if a username is available
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> UsernameIsAvailable(string username)
        {
            var account = await ctx.Accounts.SingleOrDefaultAsync(a => a.Username == username);
            return account == null;
        }
    }
}
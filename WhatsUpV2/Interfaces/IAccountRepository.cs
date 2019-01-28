using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsUpV2.EFModels;

namespace WhatsUpV2.Interfaces
{
    interface IAccountRepository
    {
        Task<Account> LogIn(string username, string password);
        Task Register(Account account);
    }
}

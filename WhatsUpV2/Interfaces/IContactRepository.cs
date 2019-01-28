using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsUpV2.EFModels;

namespace WhatsUpV2.Interfaces
{
    interface IContactRepository
    {
        IEnumerable<Contact> GetUserContacts(Account account);
        Task Add(Account account, string username);
        Contact Get(Account account, int id);
        Task<Contact> Edit(Account account, int id, string displayName);
        Task Delete(Account account, int id);
    }
}

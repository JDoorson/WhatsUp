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
        Task<IEnumerable<Contact>> GetUserContacts(int accountId);
        Task Add(Contact contact);
        //Contact Get(Account account, int id);
        Task<Contact> Edit(Account account, int id, string displayName);
        Task Delete(Account account, int id);
    }
}

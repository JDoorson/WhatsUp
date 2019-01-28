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
        Task<Contact> Get(int id);
        Task<Contact> Edit(Contact contact);
        Task Delete(int id);
    }
}

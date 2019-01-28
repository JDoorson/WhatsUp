using System;
using System.Collections.Generic;
using System.Drawing;
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
        Task<Contact> Get(int id);
        Task Edit(int id, string displayName);
        Task Delete(Contact contact);
    }
}

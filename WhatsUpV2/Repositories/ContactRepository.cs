using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhatsUpV2.Contexts;
using WhatsUpV2.EFModels;
using WhatsUpV2.Interfaces;

namespace WhatsUpV2.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly WhatsUpContext ctx = new WhatsUpContext();

        public IEnumerable<Contact> GetUserContacts(Account account)
        {
            return account.Contacts.ToList();
        }

        public Task Add(Account account, string username)
        {
            // If username is current user or username exists in contacts
            if (account.Username == username ||
                account.Contacts.SingleOrDefault(contact => contact.Username == username) != null)
            {
                return Task.CompletedTask;
            }

            // Add new contact
            account.Contacts.Add(new Contact
            {
                Username = username,
                DisplayName = username
            });

            return ctx.SaveChangesAsync();
        }

        public Task<Contact> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> Edit(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
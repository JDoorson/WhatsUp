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

        /// <summary>
        ///     Retrieve all contacts for the given user
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public IEnumerable<Contact> GetUserContacts(Account account)
        {
            return account.Contacts.ToList();
        }

        /// <summary>
        ///     Add a new contact to the given user
        /// </summary>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Retrieve a single contacts by its ID from the given user
        /// </summary>
        /// <param name="account"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contact Get(Account account, int id)
        {
            return account.GetContactById(id);
        }

        /// <summary>
        ///     Edit the display name of one of the user's contacts
        /// </summary>
        /// <param name="account"></param>
        /// <param name="id"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public async Task<Contact> Edit(Account account, int id, string displayName)
        {
            var contact = account.GetContactById(id);

            // If contact is not null, apply and save changes
            if (contact != null)
            {
                contact.DisplayName = displayName;
                await ctx.SaveChangesAsync();
            }

            return contact;
        }

        /// <summary>
        ///     Delete a contact from the given user
        /// </summary>
        /// <param name="account"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task Delete(Account account, int id)
        {
            var contact = account.GetContactById(id);

            // If contact is not null and contact could be removed
            if (contact != null && account.Contacts.Remove(contact))
            {
                return ctx.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
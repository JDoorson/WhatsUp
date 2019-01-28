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
    public class ChatRepository : IChatRepository
    {
        private readonly WhatsUpContext ctx = new WhatsUpContext();

        public async Task<IEnumerable<Chat>> GetChats(string username)
        {
            return await ctx.Chats
                .Where(c => c.UserA == username || c.UserB == username)
                .OrderByDescending(c => c.UpdatedAt)
                .ToListAsync();
        }

        public Task Create(Account account, Contact contact)
        {
            var chat = new Chat
            {
                UserA = account.Username,
                UserB = contact.Username,
                UpdatedAt = DateTime.UtcNow
            };

            ctx.Chats.Add(chat);
            return ctx.SaveChangesAsync();
        }
    }
}
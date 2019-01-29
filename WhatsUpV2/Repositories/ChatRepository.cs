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

        /// <summary>
        ///     Retrieve all chats
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Chat>> GetChats(string username)
        {
            return await ctx.Chats
                .Where(c => c.UserA == username || c.UserB == username)
                .OrderByDescending(c => c.UpdatedAt)
                .ToListAsync();
        }

        /// <summary>
        ///     Retrieve a chat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Chat> Get(int id)
        {
            return ctx.Chats.FindAsync(id);
        }
    }
}
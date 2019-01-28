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
    public class MessageRepository : IMessageRepository
    {
        private readonly WhatsUpContext ctx = new WhatsUpContext();

        public async Task<IEnumerable<Message>> GetChatMessages(int chatId)
        {
            return await ctx.Messages
                .Where(m => m.ChatId == chatId)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();
        }

        public Task Send(int chatId, string username, string text)
        {
            var message = new Message
            {
                ChatId = chatId,
                Sender = username,
                SentAt = DateTime.UtcNow
            };

            ctx.Messages.Add(message);
            return ctx.SaveChangesAsync();
        }

        public Task<Message> GetMostRecent(int chatId)
        {
            return ctx.Messages
                .Where(m => m.ChatId == chatId)
                .OrderByDescending(m => m.SentAt)
                .SingleOrDefaultAsync();
        }
    }
}
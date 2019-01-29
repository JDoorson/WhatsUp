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

        public async Task Send(int chatId, string username, string text)
        {
            var chat = await ctx.Chats.FindAsync(chatId);
            if (chat == null)
            {
                return;
            }

            var message = new Message
            {
                ChatId = chatId,
                Sender = username,
                Text = text,
                SentAt = DateTime.UtcNow
            };

            ctx.Messages.Add(message);
            chat.UpdatedAt = DateTime.UtcNow;

            await ctx.SaveChangesAsync();
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
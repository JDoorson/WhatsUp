using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsUpV2.EFModels;

namespace WhatsUpV2.Interfaces
{
    interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetChatMessages(int chatId);
        Task Send(int chatId, string username, string text);
        Task<Message> GetMostRecent(int chatId);
    }
}

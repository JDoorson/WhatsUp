using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsUpV2.EFModels;

namespace WhatsUpV2.Interfaces
{
    interface IChatRepository
    {
        Task<IEnumerable<Chat>> GetChats(string username);
        Task<Chat> Get(int chatId);
    }
}

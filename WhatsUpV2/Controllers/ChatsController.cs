using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WhatsUpV2.Interfaces;
using WhatsUpV2.Repositories;

namespace WhatsUpV2.Controllers
{
    public class ChatsController : ControllerBase
    {
        private readonly IChatRepository _chatRepository = new ChatRepository();
        private readonly IMessageRepository _messageRepository = new MessageRepository();

        /// <summary>
        ///     List all of the chats
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await _chatRepository.GetChats(GetSessionUserName()));
        }

        /// <summary>
        ///     Display a chat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Chat(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chat = await _chatRepository.Get(id.Value);
            if (chat == null)
            {
                return HttpNotFound();
            }

            // Check if user is allowed to view chat
            var username = GetSessionUserName();
            if (chat.UserA != username && chat.UserB != username)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(await _messageRepository.GetChatMessages(id.Value));
        }

        /// <summary>
        ///     Send a message to a chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Send(int chatId)
        {
            await _messageRepository.Send(chatId, GetSessionUserName(), Request.Form["messageText"]);
            return RedirectToAction("Chat", new {id = chatId});
        }
    }
}
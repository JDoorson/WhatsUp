using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhatsUpV2.Controllers
{
    public class ChatsController : ControllerBase
    {
        // GET: Chats
        public ActionResult Index()
        {
            return View();
        }
    }
}
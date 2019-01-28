using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhatsUpV2.EFModels;

namespace WhatsUpV2.Controllers
{
    public class ControllerBase : Controller
    {
        private const string SessionActiveUser = "active_user";

        protected void SetSessionUser(Account account)
        {
            Session[SessionActiveUser] = account;
        }

        protected Account GetSessionUser()
        {
            return (Account) Session[SessionActiveUser];
        }

        protected int GetSessionUserId()
        {
            return GetSessionUser().Id;
        }
    }
}
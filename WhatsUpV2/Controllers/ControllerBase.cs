using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhatsUpV2.EFModels;

namespace WhatsUpV2.Controllers
{
    /// <summary>
    ///     Class for common controller functions
    /// </summary>
    public class ControllerBase : Controller
    {
        private const string SessionActiveUser = "active_user";

        /// <summary>
        ///     Set the session user
        /// </summary>
        /// <param name="account"></param>
        protected void SetSessionUser(Account account)
        {
            Session[SessionActiveUser] = account;
        }

        /// <summary>
        ///     Retrieve the user in the session
        /// </summary>
        /// <returns></returns>
        protected Account GetSessionUser()
        {
            return (Account) Session[SessionActiveUser];
        }

        /// <summary>
        ///     Retrieve the session user's ID
        /// </summary>
        /// <returns></returns>
        protected int GetSessionUserId()
        {
            return GetSessionUser().Id;
        }

        /// <summary>
        ///     Retrieve the session user's Username
        /// </summary>
        /// <returns></returns>
        protected string GetSessionUserName()
        {
            return GetSessionUser().Username;
        }
    }
}
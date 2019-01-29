using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WhatsUpV2.Constants;
using WhatsUpV2.Contexts;
using WhatsUpV2.EFModels;
using WhatsUpV2.Interfaces;
using WhatsUpV2.Repositories;

namespace WhatsUpV2.Controllers
{
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repository = new AccountRepository();

        /// <summary>
        ///     Display login page
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIn()
        {
            return View();
        }

        /// <summary>
        ///     Handle login attempt
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> LogIn([Bind(Include = "Username,Password")] Account model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            // Retrieve the account, if null the username or password was incorrect
            var account = await _repository.LogIn(model.Username, model.Password);
            if (account == null)
            {
                ModelState.AddModelError("login-err", $"The username or password is incorrect.");
                return View(model);
            }

            // Set session data
            FormsAuthentication.SetAuthCookie(account.Username, false);
            SetSessionUser(account);

            return RedirectToAction("Index", "Contacts");
        }

        /// <summary>
        ///     Log out of the application
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            // Sign out and clear the session
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Login", "Accounts");
        }

        /// <summary>
        ///     Display the registration page
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///     Handle an account creation attempt
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Username,Password")] Account account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            await _repository.Register(account);

            return RedirectToAction("LogIn");
        }
    }
}

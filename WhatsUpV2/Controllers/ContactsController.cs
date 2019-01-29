using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhatsUpV2.Constants;
using WhatsUpV2.Contexts;
using WhatsUpV2.EFModels;
using WhatsUpV2.Interfaces;
using WhatsUpV2.Repositories;

namespace WhatsUpV2.Controllers
{
    public class ContactsController : ControllerBase
    {
        private WhatsUpContext db = new WhatsUpContext();
        private readonly IContactRepository _repository = new ContactRepository();

        /// <summary>
        ///     Get all of the current user's contacts
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await _repository.GetUserContacts(GetSessionUserId()));
        }

        /// <summary>
        ///     Display contact creation page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///     Handle contact creation
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "Username,DisplayName")] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            var account = GetSessionUser();

            // Add current user's ID as FK value
            contact.OwnerId = account.Id;
            await _repository.Add(account, contact);

            return RedirectToAction("Index", "Contacts");
        }

        /// <summary>
        ///     Display the edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contact = await _repository.Get(id.Value);

            // If not found
            if (contact == null)
            {
                return HttpNotFound();
            }

            // If it's someone else's contact, don't continue
            if (contact.OwnerId != GetSessionUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(contact);
        }

        /// <summary>
        ///     Edit a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Username,DisplayName")] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            await _repository.Edit(contact.Id, contact.DisplayName);

            return RedirectToAction("Index", "Contacts");
        }

        /// <summary>
        ///     Display the delete contact page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contact = await _repository.Get(id.Value);

            // If not found
            if (contact == null)
            {
                return HttpNotFound();
            }

            // If it's someone else's contact, don't continue
            if (contact.OwnerId != GetSessionUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(contact);
        }

        /// <summary>
        ///     Delete a contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var contact = await _repository.Get(id);

            // If not found
            if (contact == null)
            {
                return HttpNotFound();
            }

            // If it's someone else's contact, don't continue
            if (contact.OwnerId != GetSessionUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            await _repository.Delete(contact);
            
            return RedirectToAction("Index", "Contacts");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

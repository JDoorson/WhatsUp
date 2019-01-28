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

        // GET: Contacts
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

        // GET: Contacts/Edit/5
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

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Contacts/Delete/5
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

        // POST: Contacts/Delete/5
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

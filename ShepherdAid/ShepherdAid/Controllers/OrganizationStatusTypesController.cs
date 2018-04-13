using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShepherdAid.Models;
using Microsoft.AspNet.Identity;

namespace ShepherdAid.Controllers
{
    public class OrganizationStatusTypesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: OrganizationStatusTypes
        public ActionResult Index(string search)
        {
            try
            {
                var result = string.IsNullOrEmpty(search)?db.OrganizationStatusTypes: db.OrganizationStatusTypes.Where(x => x.Name.Contains(search));
                return View(result.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Display error: " + ex.InnerException.InnerException.Message;
                return View();
            }
        }

        // GET: OrganizationStatusTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationStatusTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] OrganizationStatusType organizationStatusType)
        {
            if (ModelState.IsValid)
            {
                organizationStatusType.RecordedBy = User.Identity.GetUserName();
                organizationStatusType.DateRecorded = DateTime.Now;

                db.OrganizationStatusTypes.Add(organizationStatusType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organizationStatusType);
        }

        // GET: OrganizationStatusTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationStatusType organizationStatusType = db.OrganizationStatusTypes.Find(id);
            if (organizationStatusType == null)
            {
                return HttpNotFound();
            }
            return View(organizationStatusType);
        }

        // POST: OrganizationStatusTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,RecordedBy,DateRecorded")] OrganizationStatusType organizationStatusType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationStatusType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organizationStatusType);
        }

        // GET: OrganizationStatusTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationStatusType organizationStatusType = db.OrganizationStatusTypes.Find(id);
            if (organizationStatusType == null)
            {
                return HttpNotFound();
            }
            return View(organizationStatusType);
        }

        // POST: OrganizationStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrganizationStatusType organizationStatusType = db.OrganizationStatusTypes.Find(id);
            db.OrganizationStatusTypes.Remove(organizationStatusType);
            db.SaveChanges();
            return RedirectToAction("Index");
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

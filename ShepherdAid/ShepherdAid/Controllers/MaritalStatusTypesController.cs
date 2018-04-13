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
    [AccessDeniedAuthorizeAttribute]
    public class MaritalStatusTypesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: MaritalStatusTypes
        public ActionResult Index(string search)
        {
            try
            {
                var result = string.IsNullOrEmpty(search) ? db.MaritalStatusTypes : db.MaritalStatusTypes.Where(x => x.TypeName.Contains(search));
                return View(result.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Display error: " + ex.InnerException.InnerException.Message;
                return View();
            }
        }

        // GET: MaritalStatusTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaritalStatusTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TypeName")] MaritalStatusType maritalStatusType)
        {
            if (ModelState.IsValid)
            {
                maritalStatusType.RecordedBy = User.Identity.GetUserName();
                maritalStatusType.DateRecorded = DateTime.Now;

                db.MaritalStatusTypes.Add(maritalStatusType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maritalStatusType);
        }

        // GET: MaritalStatusTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaritalStatusType maritalStatusType = db.MaritalStatusTypes.Find(id);
            if (maritalStatusType == null)
            {
                return HttpNotFound();
            }
            return View(maritalStatusType);
        }

        // POST: MaritalStatusTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TypeName,RecordedBy,DateRecorded")] MaritalStatusType maritalStatusType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maritalStatusType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maritalStatusType);
        }

        // GET: MaritalStatusTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaritalStatusType maritalStatusType = db.MaritalStatusTypes.Find(id);
            if (maritalStatusType == null)
            {
                return HttpNotFound();
            }
            return View(maritalStatusType);
        }

        // POST: MaritalStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaritalStatusType maritalStatusType = db.MaritalStatusTypes.Find(id);
            db.MaritalStatusTypes.Remove(maritalStatusType);
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

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
    public class ObligationTypesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: ObligationTypes
        public ActionResult Index(string search)
        {
            try
            {
                int id = Convert.ToInt32(Session["institution_id"]);
                var result = string.IsNullOrEmpty(search) ? db.ObligationTypes.Where(x => x.InstitutionID == id) : db.ObligationTypes.Where(x => x.InstitutionID == id && x.ObligationName.Contains(search));
                return View(result.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Display error: " + ex.InnerException.InnerException.Message;
                return View();
            }
        }

        // GET: ObligationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObligationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ObligationName")] ObligationType obligationType)
        {
            if (ModelState.IsValid)
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);
                obligationType.InstitutionID = institutionID;
                obligationType.RecordedBy = User.Identity.GetUserName();
                obligationType.DateRecorded = DateTime.Now;

                db.ObligationTypes.Add(obligationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obligationType);
        }

        // GET: ObligationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObligationType obligationType = db.ObligationTypes.Find(id);
            if (obligationType == null)
            {
                return HttpNotFound();
            }
            return View(obligationType);
        }

        // POST: ObligationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstitutionID,ObligationName,RecordedBy,DateRecorded")] ObligationType obligationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obligationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obligationType);
        }

        // GET: ObligationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObligationType obligationType = db.ObligationTypes.Find(id);
            if (obligationType == null)
            {
                return HttpNotFound();
            }
            return View(obligationType);
        }

        // POST: ObligationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObligationType obligationType = db.ObligationTypes.Find(id);
            db.ObligationTypes.Remove(obligationType);
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

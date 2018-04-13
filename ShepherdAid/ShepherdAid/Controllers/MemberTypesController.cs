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
    public class MemberTypesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: MemberTypes
        public ActionResult Index(string search)
        {
            try
            {
                int id = Convert.ToInt32(Session["institution_id"]);
                var result = string.IsNullOrEmpty(search) ? db.MemberTypes.Where(x => x.InstitutionID == id) : db.MemberTypes.Where(x => x.InstitutionID == id && x.TypeName.Contains(search));
                return View(result.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Display error: " + ex.InnerException.InnerException.Message;
                return View();
            }
        }

        // GET: MemberTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TypeName")] MemberType memberType)
        {
            if (ModelState.IsValid)
            {
                memberType.InstitutionID = Convert.ToInt32(Session["institution_id"]);
                memberType.RecordedBy = User.Identity.GetUserName();
                memberType.DateRecorded = DateTime.Now;

                db.MemberTypes.Add(memberType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberType);
        }

        // GET: MemberTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberType memberType = db.MemberTypes.Find(id);
            if (memberType == null)
            {
                return HttpNotFound();
            }
            string previousValues = AuditTrail.SetAuditTrailInfo(memberType, true);
            TempData["previousRecord"] = previousValues;

            return View(memberType);
        }

        // POST: MemberTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Edit([Bind(Include = "ID,InstitutionID,TypeName,RecordedBy,DateRecorded")] MemberType memberType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberType);
        }

        // GET: MemberTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberType memberType = db.MemberTypes.Find(id);
            if (memberType == null)
            {
                return HttpNotFound();
            }
            string previousValues = AuditTrail.SetAuditTrailInfo(memberType, false);
            TempData["previousRecord"] = previousValues;

            return View(memberType);
        }

        // POST: MemberTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberType memberType = db.MemberTypes.Find(id);
            db.MemberTypes.Remove(memberType);
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

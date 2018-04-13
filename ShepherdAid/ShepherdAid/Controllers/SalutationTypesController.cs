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
    public class SalutationTypesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: SalutationTypes
        public ActionResult Index(string search)
        {
            try
            {
                int id = Convert.ToInt32(Session["institution_id"]);
                var result = string.IsNullOrEmpty(search) ? db.SalutationTypes.Where(x => x.InstitutionID == id) : db.SalutationTypes.Where(x => x.InstitutionID == id && x.TypeName.Contains(search));
                return View(result.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Display error: " + ex.InnerException.InnerException.Message;
                return View();
            }
        }

        // GET: SalutationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalutationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TypeName")] SalutationType salutationType)
        {
            if (ModelState.IsValid)
            {
                salutationType.InstitutionID = Convert.ToInt32(Session["institution_id"]);
                salutationType.RecordedBy = User.Identity.GetUserName();
                salutationType.DateRecorded = DateTime.Now;

                db.SalutationTypes.Add(salutationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salutationType);
        }

        // GET: SalutationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalutationType salutationType = db.SalutationTypes.Find(id);
            if (salutationType == null)
            {
                return HttpNotFound();
            }
            return View(salutationType);
        }

        // POST: SalutationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstitutionID,TypeName,RecordedBy,DateRecorded")] SalutationType salutationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salutationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salutationType);
        }

        // GET: SalutationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalutationType salutationType = db.SalutationTypes.Find(id);
            if (salutationType == null)
            {
                return HttpNotFound();
            }
            return View(salutationType);
        }

        // POST: SalutationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalutationType salutationType = db.SalutationTypes.Find(id);
            db.SalutationTypes.Remove(salutationType);
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

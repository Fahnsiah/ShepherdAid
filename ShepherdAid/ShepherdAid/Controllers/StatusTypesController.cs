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
    public class StatusTypesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: StatusTypes
        public ActionResult Index(string search)
        {
            try
            {
                var result = string.IsNullOrEmpty(search) ? db.StatusTypes : db.StatusTypes.Where(x => x.StatusName.Contains(search));
                return View(result.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Display error: " + ex.InnerException.InnerException.Message;
                return View();
            }
        }

        // GET: StatusTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusType statusType = db.StatusTypes.Find(id);
            if (statusType == null)
            {
                return HttpNotFound();
            }
            return View(statusType);
        }

        // GET: StatusTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TypeName")] StatusType statusType)
        {
            if (ModelState.IsValid)
            {
                statusType.RecordedBy = User.Identity.GetUserName();
                statusType.DateRecorded = DateTime.Now;

                db.StatusTypes.Add(statusType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusType);
        }

        // GET: StatusTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusType statusType = db.StatusTypes.Find(id);
            if (statusType == null)
            {
                return HttpNotFound();
            }
            return View(statusType);
        }

        // POST: StatusTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TypeName,RecordedBy,DateRecorded")] StatusType statusType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusType);
        }

        // GET: StatusTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusType statusType = db.StatusTypes.Find(id);
            if (statusType == null)
            {
                return HttpNotFound();
            }
            return View(statusType);
        }

        // POST: StatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusType statusType = db.StatusTypes.Find(id);
            db.StatusTypes.Remove(statusType);
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

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
    public class SystemVariablesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: SystemVariables
        public ActionResult Index()
        {
            return View(db.SystemVariables.ToList());
        }

        // GET: SystemVariables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemVariable systemVariable = db.SystemVariables.Find(id);
            if (systemVariable == null)
            {
                return HttpNotFound();
            }
            return View(systemVariable);
        }

        // GET: SystemVariables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SystemVariables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Value")] SystemVariable systemVariable)
        {
            if (ModelState.IsValid)
            {
                systemVariable.RecordedBy = User.Identity.GetUserName();
                systemVariable.DateRecorded = DateTime.Now;

                db.SystemVariables.Add(systemVariable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemVariable);
        }

        // GET: SystemVariables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemVariable systemVariable = db.SystemVariables.Find(id);
            if (systemVariable == null)
            {
                return HttpNotFound();
            }
            return View(systemVariable);
        }

        // POST: SystemVariables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Value,RecordedBy,DateRecorded")] SystemVariable systemVariable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systemVariable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemVariable);
        }

        // GET: SystemVariables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemVariable systemVariable = db.SystemVariables.Find(id);
            if (systemVariable == null)
            {
                return HttpNotFound();
            }
            return View(systemVariable);
        }

        // POST: SystemVariables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SystemVariable systemVariable = db.SystemVariables.Find(id);
            db.SystemVariables.Remove(systemVariable);
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

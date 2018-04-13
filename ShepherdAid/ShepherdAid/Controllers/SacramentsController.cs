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
    public class SacramentsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: Sacraments
        public ActionResult Index()
        {
            return View(db.Sacraments.ToList());
        }

        // GET: Sacraments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sacrament sacrament = db.Sacraments.Find(id);
            if (sacrament == null)
            {
                return HttpNotFound();
            }
            return View(sacrament);
        }

        // GET: Sacraments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sacraments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,AppliedOnce,NotOnSameDay,Description")] Sacrament sacrament)
        {
            if (ModelState.IsValid)
            {
                sacrament.RecordedBy = User.Identity.GetUserName();
                sacrament.DateRecorded = DateTime.Now;

                db.Sacraments.Add(sacrament);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sacrament);
        }

        // GET: Sacraments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sacrament sacrament = db.Sacraments.Find(id);
            if (sacrament == null)
            {
                return HttpNotFound();
            }
            return View(sacrament);
        }

        // POST: Sacraments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,AppliedOnce,NotOnSameDay,Description,RecordedBy,DateRecorded")] Sacrament sacrament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sacrament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sacrament);
        }

        // GET: Sacraments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sacrament sacrament = db.Sacraments.Find(id);
            if (sacrament == null)
            {
                return HttpNotFound();
            }
            return View(sacrament);
        }

        // POST: Sacraments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sacrament sacrament = db.Sacraments.Find(id);
            db.Sacraments.Remove(sacrament);
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

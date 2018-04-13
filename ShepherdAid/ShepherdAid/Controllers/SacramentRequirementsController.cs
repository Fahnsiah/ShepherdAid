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
    public class SacramentRequirementsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: SacramentRequirements
        public ActionResult Index(int id)
        {
            Session["id"] = id;
            string sacrament = db.Sacraments.Find(id).Name;
            ViewBag.Sacrament = sacrament;
            var sacramentRequirements = db.SacramentRequirements.Where(x=>x.SacramentID==id).Include(s => s.Sacrament).Include(s => s.Sacrament1);
            return View(sacramentRequirements.ToList());
        }

        // GET: SacramentRequirements/Create
        public ActionResult Create()
        {
            int sacramentID = Convert.ToInt32(Session["id"]);
            ViewBag.SacramentID = new SelectList(db.Sacraments, "ID", "Name", sacramentID);
            ViewBag.RequirementID = new SelectList(db.Sacraments, "ID", "Name");
            return View();
        }

        // POST: SacramentRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SacramentID,RequirementID")] SacramentRequirement sacramentRequirement)
        {
            if (ModelState.IsValid)
            {
                sacramentRequirement.RecordedBy = User.Identity.GetUserName();
                sacramentRequirement.DateRecorded = DateTime.Now;

                db.SacramentRequirements.Add(sacramentRequirement);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Session["id"] });
            }

            ViewBag.SacramentID = new SelectList(db.Sacraments, "ID", "Name", sacramentRequirement.SacramentID);
            ViewBag.RequirementID = new SelectList(db.Sacraments, "ID", "Name", sacramentRequirement.RequirementID);
            return View(sacramentRequirement);
        }

        // GET: SacramentRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SacramentRequirement sacramentRequirement = db.SacramentRequirements.Find(id);
            if (sacramentRequirement == null)
            {
                return HttpNotFound();
            }
            ViewBag.SacramentID = new SelectList(db.Sacraments, "ID", "Name", sacramentRequirement.SacramentID);
            ViewBag.RequirementID = new SelectList(db.Sacraments, "ID", "Name", sacramentRequirement.RequirementID);
            return View(sacramentRequirement);
        }

        // POST: SacramentRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SacramentID,RequirementID,RecordedBy,DateRecorded")] SacramentRequirement sacramentRequirement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sacramentRequirement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Session["id"] });
            }
            ViewBag.SacramentID = new SelectList(db.Sacraments, "ID", "Name", sacramentRequirement.SacramentID);
            ViewBag.RequirementID = new SelectList(db.Sacraments, "ID", "Name", sacramentRequirement.RequirementID);
            return View(sacramentRequirement);
        }

        // GET: SacramentRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SacramentRequirement sacramentRequirement = db.SacramentRequirements.Find(id);
            if (sacramentRequirement == null)
            {
                return HttpNotFound();
            }
            return View(sacramentRequirement);
        }

        // POST: SacramentRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SacramentRequirement sacramentRequirement = db.SacramentRequirements.Find(id);
            db.SacramentRequirements.Remove(sacramentRequirement);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = Session["id"] });
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

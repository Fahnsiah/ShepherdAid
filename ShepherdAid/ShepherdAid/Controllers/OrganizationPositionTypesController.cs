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
    public class OrganizationPositionTypesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: OrganizationPositionTypes
        public ActionResult Index(string search)
        {
            try
            {
                int id = Convert.ToInt32(Session["institution_id"]);
                var result = string.IsNullOrEmpty(search) ? db.OrganizationPositionTypes.Where(x => x.InstitutionID == id) : 
                                                            db.OrganizationPositionTypes.Where(x => x.InstitutionID == id &&
                                                                (x.Name.Contains(search) ||
                                                                x.Organization.Name.Contains(search) ||
                                                                x.Description.Contains(search)
                                                                
                                                                ));
                return View(result.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Display error: " + ex.InnerException.InnerException.Message;
                return View();
            }
            var organizationPositionTypes = db.OrganizationPositionTypes.Include(o => o.Institution).Include(o => o.Organization);
            return View(organizationPositionTypes.ToList());
        }

        // GET: OrganizationPositionTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationPositionType organizationPositionType = db.OrganizationPositionTypes.Find(id);
            if (organizationPositionType == null)
            {
                return HttpNotFound();
            }
            return View(organizationPositionType);
        }

        // GET: OrganizationPositionTypes/Create
        public ActionResult Create()
        {
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", institutionID);
            ViewBag.OrgranizationID = new SelectList(db.Organizations.Where(x=>x.InstitutionID == institutionID), "ID", "Name");
            return View();
        }

        // POST: OrganizationPositionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InstitutionID,OrgranizationID,Name,Priority,Description")] OrganizationPositionType organizationPositionType)
        {
            if (ModelState.IsValid)
            {
                organizationPositionType.RecordedBy = User.Identity.GetUserName();
                organizationPositionType.DateRecorded = DateTime.Now;

                db.OrganizationPositionTypes.Add(organizationPositionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", organizationPositionType.InstitutionID);
            ViewBag.OrgranizationID = new SelectList(db.Organizations, "ID", "Name", organizationPositionType.OrgranizationID);
            return View(organizationPositionType);
        }

        // GET: OrganizationPositionTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationPositionType organizationPositionType = db.OrganizationPositionTypes.Find(id);
            if (organizationPositionType == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", organizationPositionType.InstitutionID);
            ViewBag.OrgranizationID = new SelectList(db.Organizations, "ID", "Name", organizationPositionType.OrgranizationID);
            return View(organizationPositionType);
        }

        // POST: OrganizationPositionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstitutionID,OrgranizationID,Name,Priority,Description,RecordedBy,DateRecorded")] OrganizationPositionType organizationPositionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationPositionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", organizationPositionType.InstitutionID);
            ViewBag.OrgranizationID = new SelectList(db.Organizations, "ID", "Name", organizationPositionType.OrgranizationID);
            return View(organizationPositionType);
        }

        // GET: OrganizationPositionTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationPositionType organizationPositionType = db.OrganizationPositionTypes.Find(id);
            if (organizationPositionType == null)
            {
                return HttpNotFound();
            }
            return View(organizationPositionType);
        }

        // POST: OrganizationPositionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrganizationPositionType organizationPositionType = db.OrganizationPositionTypes.Find(id);
            db.OrganizationPositionTypes.Remove(organizationPositionType);
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

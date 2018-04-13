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
    public class OrganizationsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();


        public PartialViewResult DetailsPartial()
        {
            try
            {
                int id = Convert.ToInt32(Session["id"]);
                Organization organization = db.Organizations.Find(id);
                
                return PartialView(organization);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.InnerException.InnerException.Message;
                return PartialView();
            }
        }

        // GET: Organizations
        public ActionResult Index(string search)
        {
            try
            {
                var result = string.IsNullOrEmpty(search) ? db.Organizations : db.Organizations.Where(x => x.Name.Contains(search));
                return View(result.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Display error: " + ex.Message;
                return View();
            }
        }

        // GET: Organizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["id"] = id;
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // GET: Organizations/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationStatusTypeID = new SelectList(db.OrganizationStatusTypes, "ID", "Name");
            ViewBag.OrganizationGenderTypeID = new SelectList(db.OrganizationGenderTypes, "ID", "TypeName");
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,ShortName, DateEstablished,Purpose,OrganizationGenderTypeID,OfficePhone,MobilePhone,Email,Website")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);
                organization.InstitutionID = institutionID;
                organization.OrganizationStatusTypeID = Convert.ToInt32(Enums.ObligationStatusType.Active);
                organization.RecordedBy = User.Identity.GetUserName();
                organization.DateRecorded = DateTime.Now;

                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganizationStatusTypeID = new SelectList(db.OrganizationStatusTypes, "ID", "Name", organization.OrganizationStatusTypeID);
            ViewBag.OrganizationGenderTypeID = new SelectList(db.OrganizationGenderTypes, "ID", "TypeName", organization.OrganizationGenderTypeID);
            return View(organization);
        }

        // GET: Organizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationStatusTypeID = new SelectList(db.OrganizationStatusTypes, "ID", "Name", organization.OrganizationStatusTypeID);
            ViewBag.OrganizationGenderTypeID = new SelectList(db.OrganizationGenderTypes, "ID", "TypeName", organization.OrganizationGenderTypeID);
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ShortName, DateEstablished,Purpose,OrganizationGenderTypeID,OfficePhone,MobilePhone,Email,Website,OrganizationStatusTypeID,RecordedBy,DateRecorded")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Error: Invalid model state.";
            }
            ViewBag.OrganizationStatusTypeID = new SelectList(db.OrganizationStatusTypes, "ID", "Name", organization.OrganizationStatusTypeID);
            ViewBag.OrganizationGenderTypeID = new SelectList(db.OrganizationGenderTypes, "ID", "TypeName", organization.OrganizationGenderTypeID);
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
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

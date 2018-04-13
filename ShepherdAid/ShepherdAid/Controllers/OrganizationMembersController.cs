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
    public class OrganizationMembersController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        public ActionResult Members(string search)
        {
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            int organizationID = Convert.ToInt32(Session["id"]);

            var members = db.spOrganizationNonMembers(institutionID, organizationID, search);            
           
            return View(members.ToList());
        }

        // GET: OrganizationMembers
        public PartialViewResult Index()
        {
            int id = Convert.ToInt32(Session["id"]);
            string organization = db.Organizations.Find(id).Name;
            ViewBag.Organization = organization;

            var organizationMembers = db.OrganizationMembers.Where(x => x.OrganizationID == id).Include(o => o.Member).Include(o => o.Organization).Include(o => o.StatusType);

            return PartialView(organizationMembers.ToList());
        }
        

        public ActionResult OrganizationMembers(int id)
        {
            var organizationMembers = db.OrganizationMembers.Where(x => x.OrganizationID == id).Include(o => o.Member).Include(o => o.Organization).Include(o => o.StatusType);
            return PartialView(organizationMembers.ToList());
        }

        // GET: OrganizationMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationMember organizationMember = db.OrganizationMembers.Find(id);
            if (organizationMember == null)
            {
                return HttpNotFound();
            }
            return View(organizationMember);
        }

        // GET: OrganizationMembers/Create
        public ActionResult Create(int id)
        {
            int organizationID = Convert.ToInt32(Session["id"]);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", id);
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", organizationID);
            ViewBag.StatusID = new SelectList(db.StatusTypes, "ID", "StatusName");
            return View();
        }

        // POST: OrganizationMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrganizationID,MemberID,MembershipDate,StatusID")] OrganizationMember organizationMember)
        {
            if (ModelState.IsValid)
            {
                organizationMember.RecordedBy = User.Identity.GetUserName();
                organizationMember.DateRecorded = DateTime.Now;

                db.OrganizationMembers.Add(organizationMember);
                db.SaveChanges();
                return RedirectToAction("Details", "Organizations", new { id = Session["id"] });
            }

            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", organizationMember.MemberID);
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", organizationMember.OrganizationID);
            ViewBag.StatusID = new SelectList(db.StatusTypes, "ID", "StatusName", organizationMember.StatusID);
            return View(organizationMember);
        }

        // GET: OrganizationMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationMember organizationMember = db.OrganizationMembers.Find(id);
            if (organizationMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", organizationMember.MemberID);
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", organizationMember.OrganizationID);
            ViewBag.StatusID = new SelectList(db.StatusTypes, "ID", "StatusName", organizationMember.StatusID);
            return View(organizationMember);
        }

        // POST: OrganizationMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrganizationID,MemberID,MembershipDate,StatusID,RecordedBy,DateRecorded")] OrganizationMember organizationMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Organizations", new { id = Session["id"] });
            }
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", organizationMember.MemberID);
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", organizationMember.OrganizationID);
            ViewBag.StatusID = new SelectList(db.StatusTypes, "ID", "StatusName", organizationMember.StatusID);
            return View(organizationMember);
        }

        // GET: OrganizationMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationMember organizationMember = db.OrganizationMembers.Find(id);
            if (organizationMember == null)
            {
                return HttpNotFound();
            }
            return View(organizationMember);
        }

        // POST: OrganizationMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrganizationMember organizationMember = db.OrganizationMembers.Find(id);
            db.OrganizationMembers.Remove(organizationMember);
            db.SaveChanges();
            return RedirectToAction("Details", "Organizations", new { id = Session["id"] });
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

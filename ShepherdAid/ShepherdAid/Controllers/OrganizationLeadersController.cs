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
    public class OrganizationLeadersController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();
        public ActionResult Members(string search)
        {
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            int organizationID = Convert.ToInt32(Session["id"]);

            var members = db.spOrganizationLeaderMembers(institutionID, organizationID, search);

            return View(members.ToList());
        }

        // GET: OrganizationMembers
        public PartialViewResult Index()
        {
            int id = Convert.ToInt32(Session["id"]);
            string organization = db.Organizations.Find(id).Name;
            ViewBag.Organization = organization;

            var organizationMembers = db.OrganizationLeaders.Where(x => x.OrganizationID == id).Include(o => o.Member).Include(o => o.Organization).Include(o => o.StatusType);

            return PartialView(organizationMembers.ToList());
        }

        // GET: OrganizationLeaders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationLeader organizationLeader = db.OrganizationLeaders.Find(id);
            if (organizationLeader == null)
            {
                return HttpNotFound();
            }
            return View(organizationLeader);
        }

        // GET: OrganizationLeaders/Create
        public ActionResult Create(int id)
        {
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", id);

            int organizationID = Convert.ToInt32(Session["id"]);
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", organizationID);
            ViewBag.OrganizationPositionTypeID = new SelectList(db.OrganizationPositionTypes.Where(x => x.OrgranizationID == organizationID), "ID", "Name");
            ViewBag.StatusID = new SelectList(db.StatusTypes, "ID", "StatusName");
            return View();
        }

        // POST: OrganizationLeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrganizationID,MemberID,OrganizationPositionTypeID,StartDate,EndDate,StatusID")] OrganizationLeader organizationLeader)
        {
            if (ModelState.IsValid)
            {
                organizationLeader.RecordedBy = User.Identity.GetUserName();
                organizationLeader.DateRecorded = DateTime.Now;

                db.OrganizationLeaders.Add(organizationLeader);
                db.SaveChanges();
                return RedirectToAction("Details", "Organizations", new { id = Session["id"] });
            }

            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", organizationLeader.MemberID);
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", organizationLeader.OrganizationID);
            ViewBag.OrganizationPositionTypeID = new SelectList(db.OrganizationPositionTypes, "ID", "Name", organizationLeader.OrganizationPositionTypeID);
            ViewBag.StatusID = new SelectList(db.StatusTypes, "ID", "StatusName", organizationLeader.StatusID);
            return View(organizationLeader);
        }

        // GET: OrganizationLeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationLeader organizationLeader = db.OrganizationLeaders.Find(id);
            if (organizationLeader == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", organizationLeader.MemberID);
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", organizationLeader.OrganizationID);
            ViewBag.OrganizationPositionTypeID = new SelectList(db.OrganizationPositionTypes, "ID", "Name", organizationLeader.OrganizationPositionTypeID);
            ViewBag.StatusID = new SelectList(db.StatusTypes, "ID", "StatusName", organizationLeader.StatusID);
            return View(organizationLeader);
        }

        // POST: OrganizationLeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrganizationID,MemberID,OrganizationPositionTypeID,StartDate,EndDate,StatusID,RecordedBy,DateRecorded")] OrganizationLeader organizationLeader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationLeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Organizations", new { id = Session["id"] });
            }
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", organizationLeader.MemberID);
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", organizationLeader.OrganizationID);
            ViewBag.OrganizationPositionTypeID = new SelectList(db.OrganizationPositionTypes, "ID", "Name", organizationLeader.OrganizationPositionTypeID);
            ViewBag.StatusID = new SelectList(db.StatusTypes, "ID", "StatusName", organizationLeader.StatusID);
            return View(organizationLeader);
        }

        // GET: OrganizationLeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationLeader organizationLeader = db.OrganizationLeaders.Find(id);
            if (organizationLeader == null)
            {
                return HttpNotFound();
            }
            return View(organizationLeader);
        }

        // POST: OrganizationLeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrganizationLeader organizationLeader = db.OrganizationLeaders.Find(id);
            db.OrganizationLeaders.Remove(organizationLeader);
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

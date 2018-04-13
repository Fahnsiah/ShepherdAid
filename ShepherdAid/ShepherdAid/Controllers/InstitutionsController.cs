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
    [AccessDeniedAuthorizeAttribute(Roles = StaticVariables.SuperClientInstitutionGroupAdminConstant)]
    public class InstitutionsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();
        public ActionResult Clients(string search)
        {
            var result = string.IsNullOrEmpty(search) ? db.Clients : db.Clients.Where(x => x.Name.Contains(search) || x.Initial.Contains(search));
            return View(result.ToList());
        }
        // GET: Institutions
        public ActionResult Index(int id, string search)
        {
            Session["id"] = id;

            var result = string.IsNullOrEmpty(search) ?
                db.Institutions.Where(x => x.InstitutionGroup.ClientID == id) :
                db.Institutions.Where(x => x.InstitutionGroup.ClientID == id && (x.InstitutionName.Contains(search) || x.Initial.Contains(search)));

            return View(result.ToList());
        }

        // GET: Institutions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // GET: Institutions/Create
        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["id"]);
            ViewBag.Client = db.Clients.Find(id).Name;
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x=>x.ClientID == id), "ID", "Name");
            return View();
        }

        // POST: Institutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InstitutionGroupID,InstitutionName,Initial,OfficePhone,MobilePhone,EmailAddress,Address,Website")] Institution institution)
        {
            int clientID = Convert.ToInt32(Session["id"]);
            if (ModelState.IsValid)
            {
                institution.RecordedBy = User.Identity.GetUserName();
                institution.DateRecorded = DateTime.Now;
                institution.StatusTypeID = Convert.ToInt32(Enums.StatusType.Active);

                db.Institutions.Add(institution);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = clientID });
            }

            int id = Convert.ToInt32(Session["id"]);
            ViewBag.Client = db.Clients.Find(id).Name;
            
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x => x.ClientID == clientID), "ID", "Name", institution.InstitutionGroupID);
            return View(institution);
        }

        // GET: Institutions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            int clientID = Convert.ToInt32(Session["id"]);
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x => x.ClientID == clientID), "ID", "Name", institution.InstitutionGroupID);
            ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "StatusName", institution.StatusTypeID);
            return View(institution);
        }

        // POST: Institutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstitutionGroupID,InstitutionName,Initial,OfficePhone,MobilePhone,EmailAddress,Address,Website,StatusTypeID,RecordedBy,DateRecorded")] Institution institution)
        {
            int clientID = Convert.ToInt32(Session["id"]);
            
            if (ModelState.IsValid)
            {
                db.Entry(institution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = clientID });
            }
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x => x.ClientID == clientID), "ID", "Name", institution.InstitutionGroupID);
            ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "TypeName", institution.StatusTypeID);
            return View(institution);
        }

        // GET: Institutions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Institution institution = db.Institutions.Find(id);
            db.Institutions.Remove(institution);
            db.SaveChanges();
            int clientID = Convert.ToInt32(Session["id"]);
            return RedirectToAction("Index", new { id = clientID });
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

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
   
    [AccessDeniedAuthorizeAttribute(Roles=StaticVariables.SuperClientAdminConstant)]
    public class InstitutionGroupsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: InstitutionGroups
        public ActionResult Clients(string search)
        {
            var result = string.IsNullOrEmpty(search) ? db.Clients : db.Clients.Where(x => x.Name.Contains(search) || x.Initial.Contains(search));
            return View(result.ToList());
        }
        public ActionResult Index(int id, string search)
        {
            Session["id"] = id;

            var institutionGroups = string.IsNullOrEmpty(search)? 
                db.InstitutionGroups.Where(x => x.ClientID == id):
                db.InstitutionGroups.Where(x => x.ClientID == id && (x.Name.Contains(search) || x.Initial.Contains(search)));
                
            return View(institutionGroups.ToList());
        }

        // GET: InstitutionGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionGroup institutionGroup = db.InstitutionGroups.Find(id);
            if (institutionGroup == null)
            {
                return HttpNotFound();
            }
            return View(institutionGroup);
        }

        // GET: InstitutionGroups/Create
        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["id"]);
            ViewBag.Client = db.Clients.Find(id).Name;

            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Name");
            return View();
        }

        // POST: InstitutionGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Initial,OfficePhone,MobilePhone,EmailAddress,Address,Website")] InstitutionGroup institutionGroup)
        {
            if (ModelState.IsValid)
            {
                institutionGroup.ClientID = Convert.ToInt32(Session["id"]);
                institutionGroup.RecordedBy = User.Identity.GetUserName();
                institutionGroup.DateRecorded = DateTime.Now;

                db.InstitutionGroups.Add(institutionGroup);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Session["id"] });
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Name", institutionGroup.ClientID);
            return View(institutionGroup);
        }

        // GET: InstitutionGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionGroup institutionGroup = db.InstitutionGroups.Find(id);
            if (institutionGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Name", institutionGroup.ClientID);
            return View(institutionGroup);
        }

        // POST: InstitutionGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClientID,Name,Initial,OfficePhone,MobilePhone,EmailAddress,Address,Website,RecordedBy,DateRecorded")] InstitutionGroup institutionGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(institutionGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Session["id"] });
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Name", institutionGroup.ClientID);
            return View(institutionGroup);
        }

        // GET: InstitutionGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionGroup institutionGroup = db.InstitutionGroups.Find(id);
            if (institutionGroup == null)
            {
                return HttpNotFound();
            }
            return View(institutionGroup);
        }

        // POST: InstitutionGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstitutionGroup institutionGroup = db.InstitutionGroups.Find(id);
            db.InstitutionGroups.Remove(institutionGroup);
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

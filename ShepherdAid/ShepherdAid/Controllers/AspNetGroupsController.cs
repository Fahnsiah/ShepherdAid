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
    [AccessDeniedAuthorizeAttribute(Roles = StaticVariables.SuperClientInstitutionGroupInstitutionAdminConstant)]
    public class AspNetGroupsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        public JsonResult GetInstitutionGroups(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<InstitutionGroup> result = db.InstitutionGroups.Where(x => x.ClientID == id).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInstitutions(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Institution> result = db.Institutions.Where(x => x.InstitutionGroupID == id).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: AspNetGroups
        public ActionResult Index(string search)
        {
            var result = new object();
            if (User.IsInRole(StaticVariables.SuperAdmin))
            {
                result = string.IsNullOrEmpty(search) ? db.AspNetGroups.ToList() : db.AspNetGroups.Where(x => x.Name.Contains(search)).ToList();

                return View(result);
            }
            else if (User.IsInRole(StaticVariables.ClientAdmin))
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);
                Institution institution = db.Institutions.Find(institutionID);

                result = string.IsNullOrEmpty(search) ? db.AspNetGroups.Where(x=>x.Institution.InstitutionGroup.ClientID==institution.InstitutionGroup.ClientID).ToList() :
                                                        db.AspNetGroups.Where(x => x.Institution.InstitutionGroup.ClientID == institution.InstitutionGroup.ClientID && x.Name.Contains(search)).ToList();

                return View(result);
            }
            else if (User.IsInRole(StaticVariables.InstitutionGroupAdmin))
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);
                Institution institution = db.Institutions.Find(institutionID);

                result = string.IsNullOrEmpty(search) ? db.AspNetGroups.Where(x => x.Institution.InstitutionGroupID == institution.InstitutionGroupID).ToList() :
                                                        db.AspNetGroups.Where(x => x.Institution.InstitutionGroupID == institution.InstitutionGroupID && x.Name.Contains(search)).ToList();

                return View(result);
            }
            else if (User.IsInRole(StaticVariables.InstitutionAdmin))
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);

                result = string.IsNullOrEmpty(search) ? db.AspNetGroups.Where(x => x.InstitutionID == institutionID).ToList() :
                                                        db.AspNetGroups.Where(x => x.InstitutionID == institutionID && x.Name.Contains(search)).ToList();

                return View(result);
            
            }            
                       
            return View();
        }

        public ActionResult Institutions(int? ClientID, int? InstitutionGroupID)
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Name");
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups, "ID", "Name");
            var result = db.Institutions.ToList();
            return View(result);
        }

        // GET: AspNetGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroup aspNetGroup = db.AspNetGroups.Find(id);
            if (aspNetGroup == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroup);
        }

        // GET: AspNetGroups/Create
        public ActionResult Create()
        {
            if(User.IsInRole(StaticVariables.SuperAdmin))
            {
                return RedirectToAction("SuperCreate");
            }
            else if (User.IsInRole(StaticVariables.ClientAdmin))
            {
                return RedirectToAction("ClientCreate");
            }
            else if (User.IsInRole(StaticVariables.InstitutionGroupAdmin))
            {
                return RedirectToAction("InstitutionGroupCreate");
            }
            else if (User.IsInRole(StaticVariables.InstitutionAdmin))
            {
                return RedirectToAction("InstitutionCreate");
            }
            else
            {
                return View();
            }
        }

        //[ActionName("Create")]
        public ActionResult SuperCreate()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Name");
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x=>x.ID==0), "ID", "Name");
            ViewBag.InstitutionID = new SelectList(db.Institutions.Where(x => x.ID == 0), "ID", "InstitutionName");
            return View();
        }

        //[ActionName("Create")]
        public ActionResult ClientCreate()
        {
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            Institution institution = db.Institutions.Find(institutionID);
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x => x.ClientID == institution.InstitutionGroup.ClientID), "ID", "Name");
            ViewBag.InstitutionID = new SelectList(db.Institutions.Where(x => x.ID == 0), "ID", "InstitutionName");
            return View();
        }

        //[ActionName("Create")]
        public ActionResult InstitutionGroupCreate()
        {
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            Institution institution = db.Institutions.Find(institutionID);
            ViewBag.InstitutionID = new SelectList(db.Institutions.Where(x => x.InstitutionGroupID == institution.InstitutionGroupID), "ID", "InstitutionName");
            return View();
        }
        public ActionResult InstitutionCreate()
        {
            return View();
        }

        // POST: AspNetGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsActive,RecordedBy,DateRecorded")] AspNetGroup aspNetGroup, int? InstitutionID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    aspNetGroup.RecordedBy = User.Identity.GetUserName();
                    aspNetGroup.DateRecorded = DateTime.Now;
                    aspNetGroup.InstitutionID = InstitutionID==null? Convert.ToInt32(Session["institution_id"]): Convert.ToInt32(InstitutionID);
                    db.AspNetGroups.Add(aspNetGroup);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Invalid model state.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(aspNetGroup);
        }

        // GET: AspNetGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroup aspNetGroup = db.AspNetGroups.Find(id);
            if (aspNetGroup == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroup);
        }

        // POST: AspNetGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InstitutionID,Name,IsActive,RecordedBy,DateRecorded")] AspNetGroup aspNetGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(aspNetGroup).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(aspNetGroup);
        }

        // GET: AspNetGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroup aspNetGroup = db.AspNetGroups.Find(id);
            if (aspNetGroup == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroup);
        }

        // POST: AspNetGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetGroup aspNetGroup = db.AspNetGroups.Find(id);
            db.AspNetGroups.Remove(aspNetGroup);
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

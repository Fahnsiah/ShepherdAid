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
    public class MemberSacramentsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();


        public JsonResult GetInstitutions(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Institution> result = db.Institutions.Where(x => x.InstitutionGroupID == id).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private bool RequirementsExist(int id)
        {
            var requirement = db.SacramentRequirements.Where(x => x.SacramentID == id);
            int memberID = Convert.ToInt32(Session["id"]);
            var sacrament = db.MemberSacraments.Where(x => x.MemberID == memberID);
            foreach (var item in requirement)
            {
                int count = sacrament.Select(x => x.SacramentID == item.SacramentID).Count();
                if (count.Equals(0))
                {
                    ViewData["error"] = string.Format("{0} is required before administering this sacrament.", item.Sacrament1.Name);
                    return false;
                }
            }

            return true;
        }

        // GET: MemberSacraments
        public PartialViewResult Index()
        {
            int memberID = Convert.ToInt32(Session["id"]);
            var memberSacraments = db.MemberSacraments.Where(x => x.MemberID == memberID).Include(m => m.Institution).Include(m => m.Member).Include(m => m.Sacrament);
            return PartialView(memberSacraments.ToList());
        }

        // GET: MemberSacraments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSacrament memberSacrament = db.MemberSacraments.Find(id);
            if (memberSacrament == null)
            {
                return HttpNotFound();
            }
            return View(memberSacrament);
        }

        // GET: MemberSacraments/Create
        public ActionResult Create()
        {
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            int clientID = db.Institutions.Find(institutionID).InstitutionGroup.ClientID;
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x => x.ClientID == clientID), "ID", "Name");

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "-1", Text = "" });
            ViewBag.InstitutionID = new SelectList(list, "Value", "Text");

            int memberID = Convert.ToInt32(Session["id"]);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberID);
            ViewBag.SacramentID = new SelectList(db.Sacraments, "ID", "Name");
            return View();
        }

        // POST: MemberSacraments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MemberID,SacramentID,AdministeredBy,InstitutionID,DateAdministered")] MemberSacrament memberSacrament)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (this.RequirementsExist(memberSacrament.SacramentID))
                    {
                        ShepherdAidDBEntities entity = new ShepherdAidDBEntities();
                        var isSacramentApplicable = entity.spIsSacramentApplicable(memberSacrament.MemberID, memberSacrament.SacramentID);
                        if (isSacramentApplicable != null)
                        {
                            if (Convert.ToBoolean(isSacramentApplicable.Single().Value))
                            {
                                memberSacrament.RecordedBy = User.Identity.GetUserName();
                                memberSacrament.DateRecorded = DateTime.Now;

                                db.MemberSacraments.Add(memberSacrament);
                                db.SaveChanges();
                                return RedirectToAction("Details", "Members", new { id = Session["id"] });
                            }
                            else
                            {
                                ViewData["error"] = "This sacrament is already administered and cannot be administered again.";
                            }
                        }
                        else
                        {
                            ViewData["error"] = "Error: Check sacrament applicable returns null";
                        }
                    }
                }
                catch (Exception ex)
                {

                    ViewData["error"] = "Error: " + ex.Message;
                }
            }
            else
            {
                ViewData["error"] = "Invalid model state.";
            }
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            int clientID = db.Institutions.Find(institutionID).InstitutionGroup.ClientID;
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x => x.ClientID == clientID), "ID", "Name", memberSacrament.InstitutionID);

            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", memberSacrament.InstitutionID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberSacrament.MemberID);
            ViewBag.SacramentID = new SelectList(db.Sacraments, "ID", "Name", memberSacrament.SacramentID);
            return View(memberSacrament);
        }

        // GET: MemberSacraments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSacrament memberSacrament = db.MemberSacraments.Find(id);
            if (memberSacrament == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", memberSacrament.InstitutionID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberSacrament.MemberID);
            ViewBag.SacramentID = new SelectList(db.Sacraments, "ID", "Name", memberSacrament.SacramentID);
            return View(memberSacrament);
        }

        // POST: MemberSacraments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MemberID,SacramentID,AdministeredBy,InstitutionID,DateAdministered,RecordedBy,DateRecorded")] MemberSacrament memberSacrament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberSacrament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Members", new { id = Session["id"] });
            }
            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", memberSacrament.InstitutionID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberSacrament.MemberID);
            ViewBag.SacramentID = new SelectList(db.Sacraments, "ID", "Name", memberSacrament.SacramentID);
            return View(memberSacrament);
        }

        // GET: MemberSacraments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSacrament memberSacrament = db.MemberSacraments.Find(id);
            if (memberSacrament == null)
            {
                return HttpNotFound();
            }
            return View(memberSacrament);
        }

        // POST: MemberSacraments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberSacrament memberSacrament = db.MemberSacraments.Find(id);
            db.MemberSacraments.Remove(memberSacrament);
            db.SaveChanges();
            return RedirectToAction("Details", "Members", new { id = Session["id"] });
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

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
    public class PositionTypesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: PositionTypes
        public ActionResult Index(string search)
        {
            try
            {
                int id = Convert.ToInt32(Session["institution_id"]);
                var result = string.IsNullOrEmpty(search) ? db.PositionTypes.Where(x=>x.InstitutionID == id) : db.PositionTypes.Where(x=>x.InstitutionID == id && x.Name.Contains(search));
                return View(result.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Display error: " + ex.InnerException.InnerException.Message;
                return View();
            }
        }

        // GET: PositionTypes/Create
        public ActionResult Create()
        {
           return View();
        }

        // POST: PositionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrganizationID,Name,Priority,Description")] PositionType positionType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    positionType.InstitutionID = Convert.ToInt32(Session["institution_id"]);
                    positionType.RecordedBy = User.Identity.GetUserName();
                    positionType.DateRecorded = DateTime.Now;
                    db.PositionTypes.Add(positionType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Invalid model state";
                }
            }
            catch (Exception ex)
            {
                string error = ex.InnerException.InnerException.Message.Contains("unique index")?"Cannot add duplicate record":ex.InnerException.InnerException.Message;
                ViewBag.Error = "Create error: " + error;
            }
            return View(positionType);
        }

        // GET: PositionTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionType positionType = db.PositionTypes.Find(id);
            if (positionType == null)
            {
                return HttpNotFound();
            } 
            return View(positionType);
        }

        // POST: PositionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstitutionID,OrganizationID,Name,Priority,Description,RecordedBy,DateRecorded")] PositionType positionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(positionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            return View(positionType);
        }

        // GET: PositionTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionType positionType = db.PositionTypes.Find(id);
            if (positionType == null)
            {
                return HttpNotFound();
            }

            return View(positionType);
        }

        // POST: PositionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PositionType positionType = db.PositionTypes.Find(id);
            db.PositionTypes.Remove(positionType);
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

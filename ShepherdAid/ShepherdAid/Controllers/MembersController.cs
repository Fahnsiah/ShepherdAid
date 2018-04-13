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
using DevExpress.Web.Mvc;
using System.IO;

namespace ShepherdAid.Controllers
{
    [AccessDeniedAuthorizeAttribute]
    public class MembersController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();


        public ActionResult DetailsPartial()
        {
           int id = Convert.ToInt32(Session["id"] );
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            //get the member photo into view bag
            var memberPhoto = db.MemberPhotos.Find(id);
            if (memberPhoto != null)
            {
                ViewBag.MemberPhoto = memberPhoto.DocumentPath;
                ViewBag.PhotoExist = 1;
            }
            else
            {
                ViewBag.PhotoExist = 0;
            }
            
            return View(member);
        }
        // GET: Members
        public ActionResult Index(string search)
        {
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            var members = db.Members.Where(x=>x.InstitutionID==institutionID).Include(m => m.County).Include(m => m.Institution).Include(m => m.MaritalStatusType).Include(m => m.NationalityType).Include(m => m.SalutationType).Include(m => m.StatusType);
            if(!string.IsNullOrEmpty(search))
            {
                members = members.Where(x => x.FirstName.Contains(search) ||
                                            x.MiddleName.Contains(search) ||
                                            x.LastName.Contains(search) ||
                                            x.GenderType.TypeName.Contains(search) ||
                                            x.ResidentAddress.Contains(search));
            }
            return View(members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            Session["id"] = id;

            //get the member photo into view bag
            var memberPhoto = db.MemberPhotos.Where(x=>x.MemberID==id);
            if (memberPhoto.Count()>0)
            {
                ViewBag.MemberPhoto = memberPhoto.FirstOrDefault().DocumentPath;
                ViewBag.PhotoExist = 1;
            }
            else
            {
                ViewBag.PhotoExist = 0;
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            int countryID = Convert.ToInt32(Enums.Nationality.DefaultNationality);
            ViewBag.Country = countryID;
            ViewBag.CountyID = new SelectList(db.Counties, "ID", "Name");
            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName");
            ViewBag.GenderTypeID = new SelectList(db.GenderTypes, "ID", "TypeName");
            ViewBag.MemberTypeID = new SelectList(db.MemberTypes.Where(x => x.InstitutionID == institutionID), "ID", "TypeName");
            ViewBag.MaritalStatusTypeID = new SelectList(db.MaritalStatusTypes, "ID", "TypeName");
            ViewBag.NationalityTypeID = new SelectList(db.NationalityTypes, "ID", "TypeName", countryID);
            ViewBag.SalutationTypeID = new SelectList(db.SalutationTypes.Where(x => x.InstitutionID == institutionID), "ID", "TypeName");
            ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "StatusName");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MemberID,SalutationTypeID,FirstName,MiddleName,LastName,GenderTypeID,DateOfBirth,MaritalStatusTypeID,ResidentAddress,EmailAddress,MobilePhone,OfficePhone,NationalityTypeID,CountyID,Region,MemberTypeID")] Member member)
        {
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            
            if (ModelState.IsValid)
            {
                string userID = string.Empty;

                member.InstitutionID = institutionID;

                string errorMessage = string.Empty;

                if (MyApplicationUser.CreateUser(member, ref userID, ref errorMessage))
                {
                    member.UserID = userID;

                    //set the status from enumerator
                    member.StatusTypeID = Convert.ToInt32(Enums.StatusType.Active);

                    member.RecordedBy = User.Identity.GetUserName();
                    member.DateRecorded = DateTime.Now;

                    //ensure that the member id is upper case

                    member.MemberID = member.MemberID.ToUpper();

                    db.Members.Add(member);
                    db.SaveChanges();
                    Session["id"] = member.ID;

                    return RedirectToAction("file", "PhotoUploads", new { id = member.ID });
                    //return View("PhotoUpload");
                }
                else
                {
                    ViewBag.Error = errorMessage;
                }
            }
            else
            {
                ViewBag.Error = "Invalid model state.";
            }
            
            ViewBag.CountyID = new SelectList(db.Counties, "ID", "Name", member.CountyID);
            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", member.InstitutionID);
            ViewBag.MemberTypeID = new SelectList(db.MemberTypes.Where(x => x.InstitutionID == institutionID), "ID", "TypeName", member.MemberTypeID);
            ViewBag.MaritalStatusTypeID = new SelectList(db.MaritalStatusTypes, "ID", "TypeName", member.MaritalStatusTypeID);
            ViewBag.NationalityTypeID = new SelectList(db.NationalityTypes, "ID", "TypeName", member.NationalityTypeID);
            ViewBag.SalutationTypeID = new SelectList(db.SalutationTypes.Where(x => x.InstitutionID == institutionID), "ID", "TypeName", member.SalutationTypeID);
            ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "StatusName", member.StatusTypeID);
            ViewBag.GenderTypeID = new SelectList(db.GenderTypes, "ID", "TypeName", member.GenderTypeID);
            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            Session["id"] = id;
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            ViewBag.CountyID = new SelectList(db.Counties, "ID", "Name", member.CountyID);
            ViewBag.GenderTypeID = new SelectList(db.GenderTypes, "ID", "TypeName", member.GenderTypeID);
            ViewBag.MemberTypeID = new SelectList(db.MemberTypes.Where(x => x.InstitutionID == institutionID), "ID", "TypeName", member.MemberTypeID);
            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", member.InstitutionID);
            ViewBag.MaritalStatusTypeID = new SelectList(db.MaritalStatusTypes, "ID", "TypeName", member.MaritalStatusTypeID);
            ViewBag.NationalityTypeID = new SelectList(db.NationalityTypes, "ID", "TypeName", member.NationalityTypeID);
            ViewBag.SalutationTypeID = new SelectList(db.SalutationTypes.Where(x => x.InstitutionID == institutionID), "ID", "TypeName", member.SalutationTypeID);
            ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "StatusName", member.StatusTypeID);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstitutionID,MemberID,SalutationTypeID,FirstName,MiddleName,LastName,GenderTypeID,DateOfBirth,MaritalStatusTypeID,ResidentAddress,EmailAddress,MobilePhone,OfficePhone,NationalityTypeID,CountyID,Region,MemberTypeID,StatusTypeID,RecordedBy,DateRecorded")] Member member)
        {
            if (ModelState.IsValid)
            {//ensure that the member id is in upper case
                member.MemberID = member.MemberID.ToUpper();
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int institutionID = Convert.ToInt32(Session["institution_id"]);
            ViewBag.CountyID = new SelectList(db.Counties, "ID", "Name", member.CountyID);
            ViewBag.GenderTypeID = new SelectList(db.GenderTypes, "ID", "TypeName", member.GenderTypeID);
            ViewBag.MemberTypeID = new SelectList(db.MemberTypes.Where(x => x.InstitutionID == institutionID), "ID", "TypeName", member.MemberTypeID);
            ViewBag.InstitutionID = new SelectList(db.Institutions, "ID", "InstitutionName", member.InstitutionID);
            ViewBag.MaritalStatusTypeID = new SelectList(db.MaritalStatusTypes, "ID", "TypeName", member.MaritalStatusTypeID);
            ViewBag.NationalityTypeID = new SelectList(db.NationalityTypes, "ID", "TypeName", member.NationalityTypeID);
            ViewBag.SalutationTypeID = new SelectList(db.SalutationTypes.Where(x => x.InstitutionID == institutionID), "ID", "TypeName", member.SalutationTypeID);
            ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "StatusName", member.StatusTypeID);
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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

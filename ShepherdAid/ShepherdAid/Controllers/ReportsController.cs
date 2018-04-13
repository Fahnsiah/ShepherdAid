using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShepherdAid.Controllers
{
    public class ReportsController : Controller
    {
        public ShepherdAid.Models.ShepherdAidDBEntities db = new Models.ShepherdAidDBEntities();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReportLinks()
        {
            return PartialView();
        }

        public ActionResult MembersList()
        {
            try
            {
                int id = Convert.ToInt32(Session["institution_id"]);
                var result = db.ReportMembersList(id);
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: Failed to get memebers list due to " + ex.Message;
                return View();
            }
        }
        public ActionResult DioceseMembersList(int? ParishID)
        {
            int id = Convert.ToInt32(Session["institution_id"]);
            int dioceseID = db.Institutions.Find(id).InstitutionGroupID;
            ViewBag.ParishID = new SelectList(db.Institutions.Where(x => x.InstitutionGroupID == dioceseID), "ID", "InstitutionName");
            if(ParishID==null)
            {
                return View();
            }
            var result = db.ReportMembersList(ParishID);
            return View(result);

        }
        public ActionResult ParishesMembersSummary()
        {
            try
            {
                var result = db.ReportParishMembersCount();
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: Failed to get parishes members summary due to " + ex.Message;
                return View();
            }
        }
    }
}
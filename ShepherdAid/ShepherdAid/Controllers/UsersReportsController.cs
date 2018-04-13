using ShepherdAid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShepherdAid.Controllers
{
    public class UsersReportsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();
        // GET: UsersReports
        public ActionResult Index(string search)
        {
            try
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);

                var result = string.IsNullOrEmpty(search) ? db.AspNetUsers.Where(x => x.AspNetGroup.InstitutionID == institutionID).ToList() :
                                                        db.AspNetUsers.Where(x => x.AspNetGroup.InstitutionID == institutionID &&
                                                                                    (x.FirstName.Contains(search) ||
                                                                                    x.MiddleName.Contains(search) ||
                                                                                    x.LastName.Contains(search) ||
                                                                                    x.UserName.Contains(search))).ToList();

                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }

        public ActionResult Groups(string search)
        {
            try
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);

                var result = string.IsNullOrEmpty(search) ? db.AspNetGroups.Where(x => x.InstitutionID == institutionID).ToList() :
                                                        db.AspNetGroups.Where(x => x.InstitutionID == institutionID &&
                                                                                    (x.Name.Contains(search))).ToList();

                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }

        public ActionResult UserAssignedRoles(string id)
        {
            try
            {
                var result = db.FuncReportUserAssignedRoles(id).ToList();
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }

        public ActionResult UsersAndGroups()
        {
            try
            {int institutionID = Convert.ToInt32(Session["institution_id"]);
                var result = db.FuncUsersAndGroups(institutionID).ToList();
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }

        public ActionResult GroupAssignedRoles(int id)
        {
            try
            {
                var result = db.FuncReportGroupAssignedRoles(id).ToList();
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }
    }
}
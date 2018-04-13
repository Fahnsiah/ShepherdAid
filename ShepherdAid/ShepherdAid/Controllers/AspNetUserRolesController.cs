using ShepherdAid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ShepherdAid.Controllers
{
    [AccessDeniedAuthorizeAttribute(Roles = StaticVariables.SuperClientInstitutionGroupInstitutionAdminConstant)]
    public class AspNetUserRolesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: AspNetGroupRoles
        public ActionResult Index(string search)
        {
            var result = new object();
            if (User.IsInRole(StaticVariables.SuperAdmin))
            {
                result = string.IsNullOrEmpty(search) ? db.AspNetUsers.ToList() :
                                                        db.AspNetUsers.Where(x => x.FirstName.Contains(search) ||
                                                                                    x.MiddleName.Contains(search) ||
                                                                                    x.LastName.Contains(search) ||
                                                                                    x.UserName.Contains(search)).ToList();

                return View(result);
            }
            else if (User.IsInRole(StaticVariables.ClientAdmin))
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);
                Institution institution = db.Institutions.Find(institutionID);

                result = string.IsNullOrEmpty(search) ? db.AspNetUsers.Where(x => x.AspNetGroup.Institution.InstitutionGroup.ClientID == institution.InstitutionGroup.ClientID).ToList() :
                                                        db.AspNetUsers.Where(x => x.AspNetGroup.Institution.InstitutionGroup.ClientID == institution.InstitutionGroup.ClientID && 
                                                                                    (x.FirstName.Contains(search) ||
                                                                                    x.MiddleName.Contains(search) ||
                                                                                    x.LastName.Contains(search) ||
                                                                                    x.UserName.Contains(search))).ToList();

                return View(result);
            }
            else if (User.IsInRole(StaticVariables.InstitutionGroupAdmin))
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);
                Institution institution = db.Institutions.Find(institutionID);

                result = string.IsNullOrEmpty(search) ? db.AspNetUsers.Where(x => x.AspNetGroup.Institution.InstitutionGroupID == institution.InstitutionGroupID).ToList() :
                                                        db.AspNetUsers.Where(x => x.AspNetGroup.Institution.InstitutionGroupID == institution.InstitutionGroupID &&
                                                                                    (x.FirstName.Contains(search) ||
                                                                                    x.MiddleName.Contains(search) ||
                                                                                    x.LastName.Contains(search) ||
                                                                                    x.UserName.Contains(search))).ToList();

                return View(result);
            }
            else if (User.IsInRole(StaticVariables.InstitutionAdmin))
            {
                int institutionID = Convert.ToInt32(Session["institution_id"]);

                result = string.IsNullOrEmpty(search) ? db.AspNetUsers.Where(x => x.AspNetGroup.InstitutionID == institutionID).ToList() :
                                                        db.AspNetUsers.Where(x => x.AspNetGroup.InstitutionID == institutionID &&
                                                                                    (x.FirstName.Contains(search) ||
                                                                                    x.MiddleName.Contains(search) ||
                                                                                    x.LastName.Contains(search) ||
                                                                                    x.UserName.Contains(search))).ToList();

                return View(result);

            }

            return View();
        }

        public ActionResult Roles(string id)
        {
            AspNetUser result = db.AspNetUsers.Find(id);

            if (result != null)
            {
                ViewBag.UserName = string.Format("{0} {1} {2}", result.FirstName, result.MiddleName, result.LastName);
            }
            Session["id"] = id;
            return View();
        }

        public ActionResult AssignedRoles(string id)
        {
            var result = db.spUserAssignedRoles(id).ToList();

            return PartialView(result);
        }

        public ActionResult AvailableRoles(string id)
        {
            var result = db.spUserAvailableRoles(id).ToList();
            if(User.IsInRole(StaticVariables.InstitutionAdmin))
            {
                result = result.Where(x => !(x.Name.Contains(StaticVariables.SuperAdmin) ||
                                            x.Name.Contains(StaticVariables.ClientAdmin) ||
                                            x.Name.Contains(StaticVariables.InstitutionGroupAdmin) ||
                                            x.Name.Contains(StaticVariables.InstitutionAdmin))).ToList();
                return PartialView(result);
            }
            else if (User.IsInRole(StaticVariables.InstitutionGroupAdmin))
            {
                result = result.Where(x => !(x.Name.Contains(StaticVariables.SuperAdmin) ||
                                            x.Name.Contains(StaticVariables.ClientAdmin) ||
                                            x.Name.Contains(StaticVariables.InstitutionGroupAdmin))).ToList();
                return PartialView(result);
            }
            else if (User.IsInRole(StaticVariables.ClientAdmin))
            {
                result = result.Where(x => !(x.Name.Contains(StaticVariables.SuperAdmin) ||
                                            x.Name.Contains(StaticVariables.ClientAdmin))).ToList();
                return PartialView(result);
            }
            else if (User.IsInRole(StaticVariables.SuperAdmin))
            {
                result = result.Where(x => !(x.Name.Contains(StaticVariables.SuperAdmin))).ToList();
                return PartialView(result);
            }
            return PartialView();
        }

        public ActionResult AddUserRoles(string[] availableroles)
        {
            try
            {
                string userID = Session["id"].ToString();

                //add each role to the group
                foreach (var item in availableroles)
                {
                    int result = db.spAssignRoleToUser(userID, item.ToString(), User.Identity.GetUserName());

                    if (result.Equals(0))
                    {
                        TempData["error"] = string.Format("Error assigning the role {0} to user.", db.AspNetRoles.Find(item).Name);

                        //delete all added roles
                        foreach (var role in availableroles)
                        {
                            result = db.spDeleteAddedUserRole(userID, role.ToString());

                            if (result.Equals(0))
                            {
                                TempData["error"] = "Error deleted added user role: " + db.AspNetRoles.Find(role).Name;
                                return View();
                            }

                        }

                        return View();
                    }

                }

            }
            catch (Exception ex)
            {
                TempData["error"] = "Exception Error: " + ex.Message;
            }

            return RedirectToAction("Roles", new { id = Session["id"] });
        }

        public ActionResult RevokeUserRoles(string[] assignedroles)
        {

            try
            {
                string userID = Session["id"].ToString();

                foreach (var item in assignedroles)
                {
                    int result = db.spRevokeUserRole(userID, item.ToString());

                    if (result.Equals(0))
                    {
                        TempData["error"] = string.Format("Error revoking the role {0} from user.", db.AspNetRoles.Find(item).Name);
                    }

                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Revoke user role Error: " + ex.Message;
            }

            return RedirectToAction("Roles", new { id = Session["id"] });
        }
    }
}
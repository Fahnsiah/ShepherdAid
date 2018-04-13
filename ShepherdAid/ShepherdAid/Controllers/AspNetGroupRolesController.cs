using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShepherdAid.Models;
using Microsoft.AspNet.Identity;

namespace ShepherdAid.Controllers
{
    [AccessDeniedAuthorizeAttribute(Roles = StaticVariables.SuperClientInstitutionGroupInstitutionAdminConstant)]
    public class AspNetGroupRolesController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: AspNetGroupRoles
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

                result = string.IsNullOrEmpty(search) ? db.AspNetGroups.Where(x => x.Institution.InstitutionGroup.ClientID == institution.InstitutionGroup.ClientID).ToList() :
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
            //var result = string.IsNullOrEmpty(search) ? db.AspNetGroups.ToList() : db.AspNetGroups.Where(x => x.Name.Contains(search)).ToList();

            //return View(result);
        }

        public ActionResult Roles(int id)
        {
            var roleName = db.AspNetGroups.Find(id).Name;
            ViewBag.RoleName = roleName;
            Session["id"] = id;
            return View();
        }

        public ActionResult AssignedRoles(int id)
        {
            var result = db.spGroupAssignedRoles(id).ToList();

            return PartialView(result);
        }

        public ActionResult AvailableRoles(int id)
        {
            var result = db.spGroupAvailableRoles(id).ToList();

            if (User.IsInRole(StaticVariables.InstitutionAdmin))
            {
                result = result.Where(x => !(x.Name.Contains(StaticVariables.SuperAdmin) ||
                                            x.Name.Contains(StaticVariables.ClientAdmin) ||
                                            x.Name.Contains(StaticVariables.InstitutionGroupAdmin) ||
                                            x.Name.Contains(StaticVariables.InstitutionAdmin))).ToList();
            }
            else if (User.IsInRole(StaticVariables.InstitutionGroupAdmin))
            {
                result = result.Where(x => !(x.Name.Contains(StaticVariables.SuperAdmin) ||
                                            x.Name.Contains(StaticVariables.ClientAdmin) ||
                                            x.Name.Contains(StaticVariables.InstitutionGroupAdmin))).ToList();
            }
            else if (User.IsInRole(StaticVariables.ClientAdmin))
            {
                result = result.Where(x => !(x.Name.Contains(StaticVariables.SuperAdmin) ||
                                            x.Name.Contains(StaticVariables.ClientAdmin))).ToList();
            }
            else if (User.IsInRole(StaticVariables.SuperAdmin))
            {
                result = result.Where(x => !(x.Name.Contains(StaticVariables.SuperAdmin))).ToList();
            }
            return PartialView(result);
        }

        public ActionResult AddGroupRoles(string[] availableroles)
        {
            try
            {
                int groupID = Convert.ToInt32(Session["id"]);

                //add each role to the group
                foreach (var item in availableroles)
                {
                    int result = db.spAddGroupRoles(groupID, item.ToString(), User.Identity.GetUserName());

                    if (result.Equals(0))
                    {
                        TempData["error"] = "Error adding group role: " + db.AspNetRoles.Find(item).Name;

                        //delete all added roles
                        foreach (var role in availableroles)
                        {
                            result = db.spDeleteAddedGroupRole(groupID, role.ToString());

                            if (result.Equals(0))
                            {
                                TempData["error"] = "Error deleted added group role: " + db.AspNetRoles.Find(role).Name;
                                return View();
                            }

                        } 

                        return View();
                    }

                } 
               
                //get all users in the group
                var groupUsers = db.AspNetUsers.Where(x => x.UserGroupID == groupID).ToList();
                foreach (var role in availableroles)
                {
                    foreach (var user in groupUsers)
                    {
                        int result = db.spAssignGroupAssignedRoleToUser(role, user.Id, User.Identity.GetUserName());
                        if(!result.Equals(1))
                        {
                            TempData["error"] = "Error adding group role for user.";

                            //delete all added roles
                            foreach (var item in availableroles)
                            {
                                result = db.spDeleteAddedGroupRole(groupID, item.ToString());

                                if (result.Equals(0))
                                {
                                    TempData["error"] = "Error deleted added group role: " + item.ToString();
                                    return View();
                                }

                            } 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Exception Error: " + ex.Message;
            }

            return RedirectToAction("Roles", new { id = Session["id"] });
        }

        public ActionResult RevokeGroupRoles(string[] assignedroles)
        {

            try
            {
                int groupID = Convert.ToInt32(Session["id"]);

                foreach (var item in assignedroles)
                {
                    int result = db.spRevokeGroupRole(groupID, item.ToString());

                    if (result.Equals(0))
                    {
                        TempData["error"]  = "Error revoking group role: " + item.ToString();
                    }

                }

                //get all users in the group
                var groupUsers = db.AspNetUsers.Where(x => x.UserGroupID == groupID).ToList();
                foreach (var role in assignedroles)
                {
                    foreach (var user in groupUsers)
                    {
                        db.spRevokeGroupRevokedRoleFromUser(role, user.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Revoking role error: " + ex.Message;
            }

            return RedirectToAction("Roles", new { id = Session["id"] });
        }
    }
}
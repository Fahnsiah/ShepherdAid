using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShepherdAid.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;

namespace ShepherdAid.Controllers
{
    [AccessDeniedAuthorizeAttribute]
    public class RolesController : Controller
    {
        ShepherdAidDBEntities db = new ShepherdAidDBEntities();
        // GET: Roles
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            int institutionID = 0;
            int clientID = 0;
            int instituitionGroupID = 0;

            if (User.IsInRole(StaticVariables.ClientAdmin))
            {
                instituitionGroupID = db.Institutions.Find(institutionID).InstitutionGroupID;
                clientID = db.InstitutionGroups.Find(instituitionGroupID).ClientID;
                var users = db.spApplicationUsers(clientID, instituitionGroupID, institutionID).ToList();
                return View(users);
            }
            else if (User.IsInRole(StaticVariables.InstitutionAdmin))
            {
                //only institution Id has value
                int groupID = db.AspNetUsers.Find(userID).UserGroupID;
                institutionID = db.AspNetGroups.Find(groupID).InstitutionID;
                var users = db.spApplicationUsers(clientID, instituitionGroupID, institutionID).ToList();
                return View(users);
            }
            else if (User.IsInRole(StaticVariables.SuperAdmin))
            {
                //set all the params to zero
                var users = db.spApplicationUsers(clientID, instituitionGroupID, institutionID).ToList();
                return View(users.ToList());
            }

            return View();
            //var users = db.AspNetUsers.ToList();
            //return View(users);
        }

        public ActionResult AvailableRolesList(string id)
        {
            Session["user_id"] = id;
            var model = new AvaliableRolesModel
            {
                AvailableRoles = this.GetAvailableRoles()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AvailableRolesList(AvaliableRolesModel model)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                foreach (var item in model.SelectedRoles)
                {

                    if (!this.AddUserRole(item.ToString()))
                    {
                        //redidrect to an error page
                        break;
                    }

                }

            }
            model.AvailableRoles = this.GetAvailableRoles();
            return View(model);
        }

        public ActionResult AssignedRolesList(string id)
        {
            Session["user_id"] = id;
            var model = new AvaliableRolesModel
            {
                AvailableRoles = this.GetAssignedRoles()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AssignedRolesList(AvaliableRolesModel model)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                foreach (var item in model.SelectedRoles)
                {
                    if (!this.RemoveUserFromRole(item.ToString()))
                    {
                        //redidrect to an error page
                        break;
                    }

                }

            }
            model.AvailableRoles = this.GetAssignedRoles();
            return View(model);
        }

        private IList<SelectListItem> GetAvailableRoles()
        {


            string userID = Session["user_id"].ToString();

            var paramUserID = new SqlParameter
            {
                ParameterName = "UserID",
                Value = userID
            };

            var allRoles = db.Database.SqlQuery<AssignedRolesModel>("exec GetAvailableRoles @UserID ", paramUserID);

            List<SelectListItem> roleList = new List<SelectListItem>();

            if (User.IsInRole("Super Admin"))
            {
               foreach (var item in allRoles)
                {
                    roleList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }
            else if (User.IsInRole("System Admin"))
            {
                //exclude the super admin and system admin roles
                var roles = allRoles.Where(x => x.Id != "d704556c-0624-4992-8882-15a5a710e1dc" &&
                                                x.Id != "9220F72C-A862-47EA-83B7-D31DF5F2180F");
                foreach (var item in roles)
                {
                    roleList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }
            else if (User.IsInRole("Admin"))
            {
                //exclude the super admin, system admin, group admin and admin roles
                var roles = allRoles.Where(x => x.Id != "d704556c-0624-4992-8882-15a5a710e1dc" &&
                                                x.Id != "9220F72C-A862-47EA-83B7-D31DF5F2180F" &&
                                                x.Id != "79D30350-34E0-4F1C-B75F-447C915AFCF0" &&
                                                x.Id != "3dfe8e1a-350f-4dc3-989e-07a3f5038347");
                foreach (var item in roles)
                {
                    roleList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }

            return roleList;

        }

        private IList<SelectListItem> GetAssignedRoles()
        {
            string userID = Session["user_id"].ToString();
           
            var paramUserID = new SqlParameter
            {
                ParameterName = "UserID",
                Value = userID
            };

            var roles = db.Database.SqlQuery<AssignedRolesModel>("exec GetAssignedRoles @UserID ", paramUserID);

            List<SelectListItem> roleList = new List<SelectListItem>();

            foreach (var item in roles)
            {
                roleList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            return roleList;
        }

        private bool AddUserRole(string id)
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //get the userid from the session variable
                string userID = Session["user_id"].ToString();

                //get the role name based on the id passed
                string roleName = db.AspNetRoles.Find(id).Name;

                UserManager.AddToRole(userID, roleName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool RemoveUserFromRole(string id)
        {
            try
            {
                string userID = Session["user_id"].ToString();

                var paramUserID = new SqlParameter
                {
                    ParameterName = "UserID",
                    Value = userID
                };

                var paramRoleID = new SqlParameter
                {
                    ParameterName = "RoleID",
                    Value = id
                };

                var result = db.Database.ExecuteSqlCommand("exec RemoveUserFromRole @UserID, @RoleID ", paramUserID, paramRoleID);

                if (!result.Equals(1))
                {
                    //redirect to error page
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
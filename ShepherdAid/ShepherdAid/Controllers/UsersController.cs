using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShepherdAid.Models;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;

namespace ShepherdAid.Controllers
{
    //[AccessDeniedAuthorizeAttribute(Roles = "Super Admin, System Admin, Admin")]
    public class UsersController : Controller
    {
        ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: Users
        [AccessDeniedAuthorizeAttribute(Roles = "Super Admin")]
        public ActionResult Index(int id, string search)
        {

            int institutionID = 0;
            int institutionGroupID = 0;

            int clientID = id;
            Session["client_id"] = clientID;
            ViewBag.Client = string.Format("{0} Users List", db.Clients.Find(id).Name);

            if (User.IsInRole(StaticVariables.SuperAdmin))
            {
                //result = string.IsNullOrEmpty(search) ? db.AspNetUsers : db.AspNetUsers.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search));
                var superAdmin = db.spApplicationUsers(clientID, institutionGroupID, institutionID);
                return View(superAdmin.ToList());

            }
            else
            {

                institutionID = Convert.ToInt32(Session["institution_id"]);
                //get the institution group id
                institutionGroupID = Convert.ToInt32(db.Institutions.Find(institutionID).InstitutionGroupID);
                //get the client id
                clientID = Convert.ToInt32(db.InstitutionGroups.Find(institutionGroupID).ClientID);
                Session["client_id"] = clientID;
                if (User.IsInRole(StaticVariables.ClientAdmin))
                {
                    institutionID = 0;
                    institutionGroupID = 0;
                    var client = db.spApplicationUsers(clientID, institutionGroupID, institutionID);

                    return View(client.ToList());
                }
                else if (User.IsInRole(StaticVariables.InstitutionGroupAdmin))
                {
                    institutionID = 0;
                    clientID = 0;
                    var institutionGroup = db.spApplicationUsers(clientID, institutionGroupID, institutionID);

                    return View(institutionGroup.ToList());
                }
                else if (User.IsInRole(StaticVariables.InstitutionAdmin))
                {
                    institutionGroupID = 0;
                    clientID = 0;
                    var institution = db.spApplicationUsers(clientID, institutionGroupID, institutionID);

                    return View(institution.ToList());
                }
            }

            return View();
        }
        public ActionResult Clients(string search)
        {
            if (!User.IsInRole("Super Admin"))
            {
                return RedirectToAction("Index", new { id = 0 });
            }

            var result = string.IsNullOrEmpty(search) ? db.Clients : db.Clients.Where(x => x.Name.Contains(search) || x.Initial.Contains(search));
            return View(result.ToList());
        }

        public ActionResult Details(string id)
        {
            ViewBag.DefaultPassword = db.SystemVariables.Find(Convert.ToInt32(Enums.SystemVariable.DefaultPassword)).Value;

            AspNetUser user = db.AspNetUsers.Find(id);
            return View(user);
        }
        public ActionResult Create(int id)
        {

            ViewBag.Client = string.Format("{0} Users List", db.Clients.Find(id).Name);

            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x => x.ClientID == id), "ID", "Name");

            ViewBag.InstitutionID = new SelectList(db.Institutions.Where(x => x.ID == 0), "ID", "Name");

            //int institutionID = Convert.ToInt32(Session["institution_id"]);

            ViewBag.UserGroupID = new SelectList(db.AspNetGroups.Where(x =>x.InstitutionID == 0), "ID", "Name");

            var roles = db.AspNetRoles;
            if (User.IsInRole("System Admin"))
            {
                //exclude the super admin and system admin roles
                var saroles = roles.Where(x => x.Id != "d704556c-0624-4992-8882-15a5a710e1dc" &&
                                                x.Id != "9220F72C-A862-47EA-83B7-D31DF5F2180F");
                ViewBag.RoleID = new SelectList(saroles, "Id", "Name");
            }
            else if (User.IsInRole("Admin"))
            {
                //exclude the super admin, system admin, group admin and admin roles
                var saroles = roles.Where(x => x.Id != "d704556c-0624-4992-8882-15a5a710e1dc" &&
                                                x.Id != "9220F72C-A862-47EA-83B7-D31DF5F2180F" &&
                                                x.Id != "79D30350-34E0-4F1C-B75F-447C915AFCF0" &&
                                                x.Id != "3dfe8e1a-350f-4dc3-989e-07a3f5038347");
                ViewBag.RoleID = new SelectList(saroles, "Id", "Name");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyApplicationUserModel model)
        {
            string userID = string.Empty;
            string errorMessage = string.Empty;

            if (MyApplicationUser.CreateUser(model, ref userID, ref errorMessage))
            {
                return RedirectToAction("Index", "Users", new { id = Session["client_id"] });
            }
            else
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            int id = Convert.ToInt32(Session["client_id"]);
            ViewBag.InstitutionGroupID = new SelectList(db.InstitutionGroups.Where(x => x.ClientID == id), "ID", "Name");
            ViewBag.InstitutionID = new SelectList(db.Institutions.Where(x => x.ID == model.InstitutionGroupID), "ID", "InstitutionName", model.InstitutionID);
            ViewBag.UserGroupID = new SelectList(db.AspNetGroups.Where(x => x.IsActive == true), "ID", "Name");

            var roles = db.AspNetRoles;
            if (User.IsInRole("System Admin"))
            {
                //exclude the super admin and system admin roles
                var saroles = roles.Where(x => x.Id != "d704556c-0624-4992-8882-15a5a710e1dc" &&
                                                x.Id != "9220F72C-A862-47EA-83B7-D31DF5F2180F");
                ViewBag.RoleID = new SelectList(saroles, "Id", "Name");
            }
            else if (User.IsInRole("Admin"))
            {
                //exclude the super admin, system admin, group admin and admin roles
                var saroles = roles.Where(x => x.Id != "d704556c-0624-4992-8882-15a5a710e1dc" &&
                                                x.Id != "9220F72C-A862-47EA-83B7-D31DF5F2180F" &&
                                                x.Id != "79D30350-34E0-4F1C-B75F-447C915AFCF0" &&
                                                x.Id != "3dfe8e1a-350f-4dc3-989e-07a3f5038347");
                ViewBag.RoleID = new SelectList(saroles, "Id", "Name");
            }

            ViewBag.Client = string.Format("{0} Users List", db.Clients.Find(id).Name);
            return View();
        }

        //[AccessDeniedAuthorizeAttribute(Roles = "Super Admin")]
        public ActionResult UserList()
        {
            string userID = User.Identity.GetUserId();
            int institutionID = 0;
            int clientID = 0;
            int instituitionGroupID = 0;

            if (User.IsInRole("System Admin"))
            {
                instituitionGroupID = db.Institutions.Find(institutionID).InstitutionGroupID;
                clientID = db.InstitutionGroups.Find(instituitionGroupID).ClientID;
                var users = db.spApplicationUsers(clientID, instituitionGroupID, institutionID).ToList();
                return View(users);
            }
            else if (User.IsInRole("Admin"))
            {
                //only institution Id has value
                int groupID = db.AspNetUsers.Find(userID).UserGroupID;
                institutionID = db.AspNetGroups.Find(userID).InstitutionID;
                var users = db.spApplicationUsers(clientID, instituitionGroupID, institutionID).ToList();
                return View(users);
            }
            else if (User.IsInRole("Super Admin"))
            {
                //set all the params to zero
                var users = db.spApplicationUsers(clientID, instituitionGroupID, institutionID).ToList();
                return View(users.ToList());
            }

            return View();
            //string userID = User.Identity.GetUserId();
            //int institutionID = db.InstitationUsers.Where(x => x.UserID == userID).FirstOrDefault().InstitationID;

            //if (User.IsInRole("System Admin"))
            //{
            //    int groupID = db.Institutions.Find(institutionID).InstitutionGroupID;
            //    int clientID = db.InstitutionGroups.Find(groupID).ClientID;
            //    var users = db.vwSystemUsers.Where(x => x.ClientID == clientID).ToList();
            //    return View(users);
            //}
            //else if (User.IsInRole("Admin"))
            //{
            //    var users = db.vwSystemUsers.Where(x => x.InstitationID == institutionID).ToList();
            //    return View(users);
            //}
            //else if (User.IsInRole("Super Admin"))
            //{
            //    return View(db.vwSystemUsers.ToList());
            //}

            //return View();
        }
        public JsonResult GetInstitutions(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Institution> result = db.Institutions.Where(x => x.InstitutionGroupID == id).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetGroups(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<AspNetGroup> result = db.AspNetGroups.Where(x => x.InstitutionID == id && x.IsActive == true).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public async Task<ActionResult> ResetPassword(string UserID)
        {
            //UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());



            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = userManager.FindByName("useremail@gmail.com");
            //string code = userManager.GeneratePasswordResetToken(user.Id);

            //Get the code used to reset user password
            string code = await userManager.GeneratePasswordResetTokenAsync(UserID);

            //get the default password
            string password = db.SystemVariables.Find(Convert.ToInt32(Enums.SystemVariable.DefaultPassword)).Value;
            IdentityResult result = userManager.ResetPassword(UserID, code, password);

            if (result.Succeeded)
            {
                TempData["pwdchanged"] = "Password successfully changed to " + password;
                AspNetUser user = db.AspNetUsers.Find(UserID);
                user.LoginHint = password;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Users", new { id = Session["client_id"] });

            }

            return View();

        }

        [AllowAnonymous]
        public ActionResult Denied()
        {
            ViewBag.PrevPage = Request.RawUrl;
            return View();
        }

    }
}
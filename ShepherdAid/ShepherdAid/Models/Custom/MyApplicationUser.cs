using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ShepherdAid.Models;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace ShepherdAid.Models
{
    public class MyApplicationUser
    {

        private static bool UserNameExists(string UserName)
        {
            ShepherdAidDBEntities db = new ShepherdAidDBEntities();

            int count = db.AspNetUsers.Where(x => x.UserName == UserName).Count();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static string GetUserName(MyApplicationUserModel model)
        {
            try
            {
                string username = string.Empty;

                if (string.IsNullOrEmpty(model.MiddleName))
                {
                    username = string.Format("{0}{1}", (model.FirstName).Trim().Substring(0, 1), model.LastName.Trim());
                }
                else
                {
                    username = string.Format("{0}{1}{2}", (model.FirstName).Trim().Substring(0, 1),
                                                            (model.MiddleName).Trim().Substring(0, 1),
                                                            model.LastName.Trim());

                }

                int cnt = 1;

                string availableName = username.ToLower();
                while (UserNameExists(availableName))
                {
                    availableName = string.Format("{0}{1}", username, cnt);
                    cnt++;
                }
                return availableName;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static string GetUserName(Member model)
        {
            try
            {
                string username = string.Empty;

                if (string.IsNullOrEmpty(model.MiddleName))
                {
                    username = string.Format("{0}{1}", (model.FirstName).Trim().Substring(0, 1), model.LastName.Trim());
                }
                else
                {
                    username = string.Format("{0}{1}{2}", (model.FirstName).Trim().Substring(0, 1),
                                                            (model.MiddleName).Trim().Substring(0, 1),
                                                            model.LastName.Trim());

                }

                int cnt = 1;

                string availableName = username.ToLower();
                while (UserNameExists(availableName))
                {
                    availableName = string.Format("{0}{1}", username, cnt);
                    cnt++;
                }
                return availableName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool  GetGroupID(int id, ref object result)
        {
            try
            {
                ShepherdAidDBEntities db = new ShepherdAidDBEntities();

                //var IDS = db.spGetDefaultInstitutionGroupCount(id, StaticVariables.DefaultUserGroup);
                int groupID = db.AspNetGroups.Where(x => x.InstitutionID == id && x.Name == StaticVariables.DefaultUserGroup).Count();

                if (groupID < 1)
                {
                    AspNetGroup aspNetGroup = new AspNetGroup();
                    aspNetGroup.InstitutionID = id;
                    aspNetGroup.Name = StaticVariables.DefaultUserGroup;
                    aspNetGroup.RecordedBy = "Admin";
                    aspNetGroup.DateRecorded = DateTime.Now;
                    db.AspNetGroups.Add(aspNetGroup);
                    db.SaveChanges();

                    groupID = aspNetGroup.ID;
                }
                result = groupID;
                return true;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
        }
        //private static bool AddInstitutionalUser(string userid, int InstititutionID)
        //{
        //    try
        //    {
        //        ShepherdAidDBEntities db = new ShepherdAidDBEntities();
        //        InstitationUser institutionUser = new InstitationUser();
        //        institutionUser.InstitationID = InstititutionID;
        //        institutionUser.UserID = userid;
        //        institutionUser.StatusTypeID = Convert.ToInt32(Enums.StatusType.Active);
        //        institutionUser.RecordedBy = "Admin";
        //        institutionUser.DateRecorded = DateTime.Now;
        //        db.InstitationUsers.Add(institutionUser);
        //        db.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}
        private static string GetDefaultPassword()
        {
            Random random = new Random();
            string[] passwords = new string[] { "P@ssw", "Pa$$w", "Pa55w", "P@55w", "Pa$5w", "P@5$w" };
            string defaultPassword = passwords[random.Next(0, passwords.Length)];
            defaultPassword = string.Format("{0}{1}d", defaultPassword, random.Next(1000, 9999));
            return defaultPassword;
        }
        public static bool CreateUser(MyApplicationUserModel model, ref string pUserID, ref string pErrorMessage)
        {
            ShepherdAidDBEntities db = new ShepherdAidDBEntities();
            try
            {

                var user = new ApplicationUser()
                {
                    UserName = GetUserName(model),
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserGroupID = model.UserGroupID

                };

                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                string userPWD = GetDefaultPassword();
                user.LoginHint = userPWD;

                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    pUserID = user.Id;
                    int institutionID = model.InstitutionID;

                    if (model.InstitutionID.Equals(0))
                    {
                        string userID = HttpContext.Current.User.Identity.GetUserId();
                        int groupID = db.AspNetUsers.Find(userID).UserGroupID;
                        institutionID = db.AspNetGroups.Find(groupID).InstitutionID;
                    }

                    var result = db.spAddCreatedUserRoles(model.UserGroupID, pUserID);

                    if (Convert.ToInt32(result.FirstOrDefault().Value) < 0)
                    {
                        //AspNetUser appUser = db.AspNetUsers.Find(pUserID);
                        //db.AspNetUsers.Remove(appUser);
                        //db.SaveChanges();
                        UserManager.Delete(user);
                        pErrorMessage = "Error: Failed to assign roles to created user.";
                        return false;
                    }
                }//User created successfully

                return true;
            }
            catch (Exception ex)
            {
                //Redirect to error page
                ShepherdAidDBEntities entity = new ShepherdAidDBEntities();
                AspNetUser user = entity.AspNetUsers.Find(pUserID);
                entity.AspNetUsers.Remove(user);
                entity.SaveChanges();

                pErrorMessage = "Error " + ex.Message;
                return false;
            }
        }

        public static bool CreateUser(Member model, ref string pUserID, ref string pErrorMessage)
        {
            ShepherdAidDBEntities db = new ShepherdAidDBEntities();
            try
            {
                object groupIDResult = new object();
                if (!GetGroupID(model.InstitutionID, ref groupIDResult))
                {
                    pErrorMessage = groupIDResult.ToString();
                    return false;
                }
                int groupID = Convert.ToInt32(groupIDResult);
                
                var user = new ApplicationUser()
                {
                    UserName = GetUserName(model),
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Email = model.EmailAddress,
                    UserGroupID = groupID
                    

                };

                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                string userPWD = GetDefaultPassword();
                user.LoginHint = userPWD;
                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var result = db.spAddCreatedUserRoles(user.UserGroupID, user.Id);

                    if (result.Single().Value  < 0)
                    {
                        //AspNetUser appUser = db.AspNetUsers.Find(pUserID);
                        //db.AspNetUsers.Remove(appUser);
                        //db.SaveChanges();
                        UserManager.Delete(user);
                        pErrorMessage = "Error: Failed to assign roles to created user.";
                        return false;
                    }
                    //string role = StaticVariables.DefaultUserGroup;

                    //var result = UserManager.AddToRole(user.Id, role);

                    //if (!result.Succeeded)
                    //{
                    //    UserManager.Delete(user);

                    //    //redirect to error page
                    //    return false;
                    //}

                    //get selected institution or get admin institution

                    int institutionID = model.InstitutionID;

                    if (model.InstitutionID.Equals(0))
                    {
                        string userID = HttpContext.Current.User.Identity.GetUserId();
                        groupID = db.AspNetUsers.Find(userID).UserGroupID;
                        institutionID = db.AspNetGroups.Find(groupID).InstitutionID;
                    }

                    ////Add to institution
                    //if (!AddInstitutionalUser(user.Id, institutionID))
                    //{
                    //    //Remove the added user
                    //    UserManager.Delete(user);

                    //    //Redire to error page
                    //    return false;
                    //}

                    //get the user id to return to caller
                    pUserID = user.Id;
                }

                return true;
            }
            catch (Exception ex)
            {
                pErrorMessage = ex.Message;
                //Redirect to error page
                return false;
            }
        }

    }

    public class MyApplicationUserModel
    {
        [Key]
        public string ID { get; set; }

        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Role Name")]
        public string RoleID { get; set; }

        [Display(Name = "Inst. Group")]
        public int InstitutionGroupID { get; set; }

        [Display(Name = "Inst. Name")]
        public int InstitutionID { get; set; }

        [Display(Name = "User Group")]
        public int UserGroupID { get; set; }
    }


    public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
    {
        ShepherdAidDBEntities db = new ShepherdAidDBEntities();
        private bool HasDefaultPasswordChanged(string userID)
        {

            ShepherdAidDBEntities db = new ShepherdAidDBEntities();
            var user = db.AspNetUsers;
            if (user != null)
            {
                if (user.FirstOrDefault().LoginHint == null)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            string loginHint = db.AspNetUsers.Find(userID).LoginHint;
            if (string.IsNullOrEmpty(loginHint))
            {
                return true;
            }

            return false;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //check on default password change
                string userID = filterContext.HttpContext.User.Identity.GetUserId();
                AspNetUser netUser = db.AspNetUsers.Find(userID);

                if(netUser == null)
                {
                    filterContext.Result = new RedirectResult("~/Account/Login");
                    return;
                }
                else
                {
                    if (!HasDefaultPasswordChanged(userID))
                    {
                        filterContext.Result = new RedirectResult("~/Account/Manage");
                        return;
                    }
                }
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                //authenticated but does not have access
                filterContext.Result = new RedirectResult("~/Users/Denied");
            }
        }
    }
}
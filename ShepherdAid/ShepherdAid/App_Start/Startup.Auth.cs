using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using ShepherdAid.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShepherdAid
{
    public partial class Startup
    {
        ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        private bool createDefaultInstitution(ref int InstitutionID)
        {
            try
            {
                //Set the instituion id to zero
                InstitutionID = 0;
                Company company = null;
                Client client = null;
                InstitutionGroup institutionGroup = null;
                Institution institution = null;

                //Create Company
                try
                {
                    
                    company = new Company()
                    {
                        Name = "RESEARCH INSTITUTE OF TECHNOLOGICAL ENTREPRENUERS",
                        Initial = "RITE",
                        Address = "Monrovia, Liberia",
                        EmailAddress = "info@riteliberia.org",
                        OfficePhone = "+231776943923",
                        MobilePhone = "+231776943923",
                        RecordedBy = "Developer",
                        DateRecorded = DateTime.Now
                    };
                    db.Companies.Add(company);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }

                //Add default client
                try
                {
                    client = new Client()
                    {
                        CompanyID = company.ID,
                        Name = "Catholic Bishops Conference of Liberia",
                        Initial = "CABICOL",
                        Address = "Monrovia, Liberia",
                        EmailAddress = "info@cabicol.org",
                        OfficePhone = "+231...",
                        MobilePhone = "+231...",
                        RecordedBy = "Developer",
                        DateRecorded = DateTime.Now
                    };
                    db.Clients.Add(client);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    //Delete company
                    db.Companies.Remove(company);
                    db.SaveChanges(); 
                    return false;
                }

                //Add the default institution group
                try
                {
                    institutionGroup = new InstitutionGroup()
                            {
                                ClientID = client.ID,
                                Name = "Catholic Archdiocese of Monrovia",
                                Initial = "",
                                Address = "Monrovia, Liberia",
                                EmailAddress = "info@cabicol.org",
                                OfficePhone = "+231...",
                                MobilePhone = "+231...",
                                RecordedBy = "Developer",
                                DateRecorded = DateTime.Now
                            };
                    db.InstitutionGroups.Add(institutionGroup);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    //Delete company and client
                    db.Companies.Remove(company);
                    db.SaveChanges(); 

                    db.Clients.Remove(client);
                    db.SaveChanges(); 
                    return false;
                }

                //Create the Default Institution
                try
                {
                    institution = new Institution()
                            {
                                InstitutionGroupID = institutionGroup.ID,
                                InstitutionName = "Holy Innocents Parish",
                                Initial = "HIP",
                                Address = "Monrovia, Liberia",
                                EmailAddress = "info@riteliberia.org",
                                StatusTypeID = Convert.ToInt32(Enums.StatusType.Active),
                                OfficePhone = "+231776943923",
                                MobilePhone = "+231776943923",
                                RecordedBy = "Developer",
                                DateRecorded = DateTime.Now
                            };
                    db.Institutions.Add(institution);
                    db.SaveChanges();

                    InstitutionID = institution.ID;
                }
                catch (Exception)
                {
                     //Delete company, client and institution group
                    db.Companies.Remove(company);
                    db.SaveChanges(); 

                    db.Clients.Remove(client);
                    db.SaveChanges();  

                    db.InstitutionGroups.Remove(institutionGroup);
                    db.SaveChanges(); 
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                //delete all added records                
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //private bool AddInstitutionalUser(string userid, int InstititutionID)
        //{
        //    try
        //    {
        //        ShepherdAidDBEntities db = new ShepherdAidDBEntities();
        //        InstitationUser institutionUser = new InstitationUser();
        //        institutionUser.InstitationID = InstititutionID;
        //        institutionUser.UserID = userid;
        //        institutionUser.StatusTypeID = Convert.ToInt32(Enums.StatusType.Active);
        //        institutionUser.RecordedBy = "superadmin";
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
        public bool deleteInstitution(int id)
        {
            try
            {
                ShepherdAidDBEntities db = new ShepherdAidDBEntities();
                Institution institution = db.Institutions.Find(id);
                db.Institutions.Remove(institution);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool deleteRole(string id)
        {
            try
            {
                ShepherdAidDBEntities db = new ShepherdAidDBEntities();
                AspNetRole appRole = db.AspNetRoles.Find(id);
                db.AspNetRoles.Remove(appRole);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        private bool createDefaultRolesandUsers()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


                // create super admin role and the super admin   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();

                if (!roleManager.RoleExists("Super Admin"))
                {
                    //Create default Institution 
                    int institutionID = 0;
                    if (!createDefaultInstitution(ref institutionID))
                    {
                        return false;
                    }

                    AspNetGroup group = new AspNetGroup(){
                        Name = "SUPER ADMIN",
                        InstitutionID = institutionID,
                        IsActive = true,
                        RecordedBy = "Developer",
                        DateRecorded = DateTime.Now,
                    };

                    db.AspNetGroups.Add(group);
                    db.SaveChanges();

                    // first we create super Admin role   
                    role.Name = "Super Admin";
                    roleManager.Create(role);

                    //Here we create a Admin super user who will maintain the website  
                    var user = new ApplicationUser();
                    user.FirstName = "Super";
                    user.LastName = "Administrator";
                    user.UserName = "superadmin";
                    user.Email = "info@riteliberia.org";
                    user.PhoneNumber = "+231776943923";
                    user.UserGroupID = group.ID;

                    string userPWD = "P@55w0rd";

                    var chkUser = UserManager.Create(user, userPWD);

                    //Add default User to Role Admin   
                    if (chkUser.Succeeded)
                    {
                        try
                        {
                            var result1 = UserManager.AddToRole(user.Id, "Super Admin");
                        }
                        catch (Exception)
                        {
                            //delete created group, user and role
                            db.AspNetGroups.Remove(group);
                            db.SaveChanges();

                            if (!this.deleteRole(role.Id))
                            {
                                return false;
                            }

                            AspNetUser netUser = db.AspNetUsers.Find(user.Id);
                            db.AspNetUsers.Remove(netUser);
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        if (!this.deleteRole(role.Id))
                        {
                            return false;
                        }

                        //Redirect to an error page.                        
                    }

                    if (!roleManager.RoleExists("Client Admin"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Client Admin";
                        roleManager.Create(role);
                    }

                    if (!roleManager.RoleExists("Institution Group Admin"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Institution Group Admin";
                        roleManager.Create(role);
                    }

                    if (!roleManager.RoleExists("Institution Admin"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Institution Admin";
                        roleManager.Create(role);
                    }

                    if (!roleManager.RoleExists("Admin Data Entry"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Admin Data Entry";
                        roleManager.Create(role);
                    }

                    if (!roleManager.RoleExists("Admin Data Change"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Admin Data Change";
                        roleManager.Create(role);
                    }
                    if (!roleManager.RoleExists("Admin Data Delete"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Admin Data Delete";
                        roleManager.Create(role);
                    }

                    if (!roleManager.RoleExists("User Data Entry"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "User Data Entry";
                        roleManager.Create(role);
                    }

                    if (!roleManager.RoleExists("User Data Change"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "User Data Change";
                        roleManager.Create(role);
                    }
                    if (!roleManager.RoleExists("User Data Delete"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "User Data Delete";
                        roleManager.Create(role);
                    }

                    if (!roleManager.RoleExists("Audit Trail"))
                    {
                        role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Audit Trail";
                        roleManager.Create(role);
                    }

                }//super admin not exist

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
            if (!createDefaultRolesandUsers())
            {
                //Redirect to an error page

            }
        }

      
    }
}
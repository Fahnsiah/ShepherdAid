using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using ShepherdAid.Models;

namespace ShepherdAid.Controllers
{
    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Tracker tracker = new Tracker();
            var request = filterContext.HttpContext.Request;
            tracker.UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous";
            tracker.IpAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            tracker.AreaAccessed = request.RawUrl;
            tracker.ActionDate = DateTime.UtcNow;
            tracker.PreviousValues = Convert.ToString(filterContext.Controller.TempData["previousRecord"]);
            // Stores the Audit in the Database
            ShepherdAidDBEntities context = new ShepherdAidDBEntities();
            context.Trackers.Add(tracker);
            context.SaveChanges();
            filterContext.Controller.TempData["previousRecord"] = null;        

                base.OnActionExecuted(filterContext);
        }

       

    }

    public class AuditTrail
    {
        public static string SetAuditTrailInfo(object pObject, bool IsModifyAction)
        {
            string previousRecords = IsModifyAction?"Modification: ":"Delete: ";
            try
            {
                Type EntityType = pObject.GetType();
                PropertyInfo[] propInfo = EntityType.GetProperties();

                foreach (var item in propInfo)
                {
                    previousRecords += item.Name + " : " + item.GetValue(pObject, null) + " ; ";
                }


            }
            catch (Exception)
            {
                //ToDo:  Redirect to an error page
            }
            return previousRecords;

        }
    }

}
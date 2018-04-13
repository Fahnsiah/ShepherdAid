using ShepherdAid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShepherdAid.Controllers
{
    public class PaymentsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        // GET: Payments
        public ActionResult Index()
        {
            return View();
        }
        
        public PartialViewResult MemberPartial(string MemberID, string FirstName, string LastName)
        {
            var result = db.spFilterMembersList(MemberID, FirstName, LastName).ToList();
            return PartialView(result);
        }        

        public PartialViewResult MemberPayment(int id)
        {
            var result = db.spMemberPaymentList(id);

            if (result != null)
            {
                ViewBag.Amount = result.FirstOrDefault().Amount;
                ViewBag.TotalPayment = result.FirstOrDefault().TotalPayment;
                ViewBag.DateStarted = result.FirstOrDefault().DateStarted;
                ViewBag.AccumAmount = result.FirstOrDefault().AccumAmount;
            }
            return PartialView();
        }
    }
}
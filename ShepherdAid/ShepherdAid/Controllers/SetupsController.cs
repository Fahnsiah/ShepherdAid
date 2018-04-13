using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShepherdAid.Models;

namespace ShepherdAid.Controllers
{
    [AccessDeniedAuthorizeAttribute]
    public class SetupsController : Controller
    {
        // GET: Setups
        public ActionResult Index()
        {
            return View();
        }
    }
}
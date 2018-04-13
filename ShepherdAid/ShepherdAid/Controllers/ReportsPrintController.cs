using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace ShepherdAid.Controllers
{
    [Authorize]
    public class ReportsPrintController : Controller
    {
        // GET: ReportsPrint
        public ActionResult MembersList()
        {
            return View();
        }

        ShepherdAid.XtraReports.ReportsDesigns.xrptMembersList report = null;
        public ActionResult ReportMembersListPartial()
        {
            int id = Convert.ToInt32(Session["institution_id"]);
            report = new ShepherdAid.XtraReports.ReportsDesigns.xrptMembersList(id);
            return PartialView("_ReportMembersListPartial", report);
        }

        public ActionResult ReportMembersListPartialExport()
        {
            int id = Convert.ToInt32(Session["institution_id"]);
            report = new ShepherdAid.XtraReports.ReportsDesigns.xrptMembersList(id);
            return DocumentViewerExtension.ExportTo(report, Request);
        }
    }
}
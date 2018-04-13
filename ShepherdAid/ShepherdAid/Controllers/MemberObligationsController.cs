using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShepherdAid.Models;
using Microsoft.AspNet.Identity;

namespace ShepherdAid.Controllers
{
    public class MemberObligationsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        private decimal ConvertedAmount { get; set; }
        private decimal AdjustedAmount { get; set; }

        private bool MakeAdjustment(int id, int recurranceTypeID, int modifiedCurrencyTypeID, decimal modifiedAmount, DateTime modifiedDateStarted)
        {
            try
            {
                this.AdjustedAmount = 0;
                //get the current obligation to make comparison with the changes made
                //var memObligation = db.MemberObligations.Where(x=>x.ID==id).SingleOrDefault();
                var memObligation = (MemberObligation)TempData["member_obligation"];

                if (!modifiedCurrencyTypeID.Equals(memObligation.CurrencyTypeID))//do convertions and make necesary adjustment
                {
                    int svid = Convert.ToInt32(Enums.SystemVariable.LRDToUSDRate);
                    string exchangeRate = db.SystemVariables.Find(svid).Value;
                    string[] exchangeRateParts = exchangeRate.Split(':');
                    decimal usdRateToLrd = 0;
                    if (exchangeRateParts.Length.Equals(2))
                    {
                        usdRateToLrd = Convert.ToDecimal(exchangeRateParts[1]) / Convert.ToDecimal(exchangeRateParts[0]);
                    }

                    Enums.Currency currencyType = (Enums.Currency)memObligation.CurrencyTypeID;
                    switch (currencyType)
                    {
                        case Enums.Currency.LRD://changing from lrd to usd
                            this.ConvertedAmount = modifiedAmount / usdRateToLrd;
                            this.AdjustedAmount = memObligation.AdjustedBalance / usdRateToLrd;
                            break;
                        case Enums.Currency.USD://changing from usd to lrd
                            this.ConvertedAmount = modifiedAmount * usdRateToLrd;
                            this.AdjustedAmount = memObligation.AdjustedBalance * usdRateToLrd;
                            break;
                        default:
                            break;
                    }
                }

                long duration = 0;
                //Microsoft.VisualBasic.VBCodeProvider 
                Enums.ObligationFrequency obligationFreq = (Enums.ObligationFrequency)recurranceTypeID;
                switch (obligationFreq)
                {
                    case Enums.ObligationFrequency.Monthly:

                        duration = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, memObligation.DateStarted, modifiedDateStarted);
                        break;

                    case Enums.ObligationFrequency.Yearly:

                        duration = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Year, memObligation.DateStarted, modifiedDateStarted);
                        break;
                }

                if (!duration.Equals(0))
                {
                    this.AdjustedAmount = this.AdjustedAmount * duration;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // GET: MemberObligations
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["id"]);
            var memberObligations = db.MemberObligations.Where(x => x.MemberID == id).Include(m => m.CurrencyType).Include(m => m.Member).Include(m => m.ObligationStatusType).Include(m => m.ObligationType).Include(m => m.RecurranceType);
            return PartialView(memberObligations.ToList());
        }

        // GET: MemberObligations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberObligation memberObligation = db.MemberObligations.Find(id);
            if (memberObligation == null)
            {
                return HttpNotFound();
            }
            return View(memberObligation);
        }

        // GET: MemberObligations/Create
        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["id"]);

            ViewBag.CurrencyTypeID = new SelectList(db.CurrencyTypes, "ID", "CurrencyName");
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", id);
            ViewBag.ObligationStatusTypeID = new SelectList(db.ObligationStatusTypes, "ID", "StatusName");
            ViewBag.ObligationTypeID = new SelectList(db.ObligationTypes, "ID", "ObligationName");
            ViewBag.RecurranceTypeID = new SelectList(db.RecurranceTypes, "ID", "Recurrance");
            return View();
        }

        // POST: MemberObligations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MemberID,ObligationTypeID,CurrencyTypeID,Amount,DateStarted,RecurranceTypeID")] MemberObligation memberObligation)
        {
            if (ModelState.IsValid)
            {
                long duration = 0;
                //Microsoft.VisualBasic.VBCodeProvider 
                Enums.ObligationFrequency obligationFreq = (Enums.ObligationFrequency)memberObligation.RecurranceTypeID;
                switch (obligationFreq)
                {
                    case Enums.ObligationFrequency.Once:
                        memberObligation.AdjustedBalance = memberObligation.Amount;
                        break;

                    case Enums.ObligationFrequency.Monthly:

                        duration = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, memberObligation.DateStarted, DateTime.Now);
                        memberObligation.AdjustedBalance = duration * memberObligation.Amount;
                        break;

                    case Enums.ObligationFrequency.Yearly:

                        duration = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Year, memberObligation.DateStarted, DateTime.Now);
                        memberObligation.AdjustedBalance = duration * memberObligation.Amount;
                        break;

                }

                string adjustedDate = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, memberObligation.DateStarted.Day);
                memberObligation.DateAdjusted = Convert.ToDateTime(adjustedDate);
                memberObligation.ObligationStatusTypeID = Convert.ToInt32(Enums.ObligationStatus.Active);
                memberObligation.RecordedBy = User.Identity.GetUserName();
                memberObligation.DateRecorded = DateTime.Now;

                db.MemberObligations.Add(memberObligation);
                db.SaveChanges();
                return RedirectToAction("Details", "Members", new { id = Session["id"] });
            }

            ViewBag.CurrencyTypeID = new SelectList(db.CurrencyTypes, "ID", "CurrencyName", memberObligation.CurrencyTypeID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberObligation.MemberID);
            ViewBag.ObligationStatusTypeID = new SelectList(db.ObligationStatusTypes, "ID", "StatusName", memberObligation.ObligationStatusTypeID);
            ViewBag.ObligationTypeID = new SelectList(db.ObligationTypes, "ID", "ObligationName", memberObligation.ObligationTypeID);
            ViewBag.RecurranceTypeID = new SelectList(db.RecurranceTypes, "ID", "Recurrance", memberObligation.RecurranceTypeID);
            return View(memberObligation);
        }

        // GET: MemberObligations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberObligation memberObligation = db.MemberObligations.Find(id);

            TempData["previousRecord"] = memberObligation;
            if (memberObligation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrencyTypeID = new SelectList(db.CurrencyTypes, "ID", "CurrencyName", memberObligation.CurrencyTypeID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberObligation.MemberID);
            ViewBag.ObligationStatusTypeID = new SelectList(db.ObligationStatusTypes, "ID", "StatusName", memberObligation.ObligationStatusTypeID);
            ViewBag.ObligationTypeID = new SelectList(db.ObligationTypes, "ID", "ObligationName", memberObligation.ObligationTypeID);
            ViewBag.RecurranceTypeID = new SelectList(db.RecurranceTypes, "ID", "Recurrance", memberObligation.RecurranceTypeID);
            return View(memberObligation);
        }

        // POST: MemberObligations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MemberID,ObligationTypeID,CurrencyTypeID,Amount,AdjustedBalance,DateStarted,DateAdjusted,RecurranceTypeID,ObligationStatusTypeID,RecordedBy,DateRecorded")] MemberObligation memberObligation)
        {
            if (ModelState.IsValid)
            {
                //int id = memberObligation.ID;
                //DateTime modifiedDate = memberObligation.DateStarted;
                //int recurranceTypeID = memberObligation.RecurranceTypeID;
                //int modifiedCurrencyTypeID = memberObligation.CurrencyTypeID;
                //decimal modifiedAmount = memberObligation.Amount;

                MemberObligation memObligation = (MemberObligation)TempData["previousRecord"];

                long duration = 0;
                Enums.ObligationFrequency obligationFreq = (Enums.ObligationFrequency)memObligation.RecurranceTypeID;
                if (!memberObligation.DateStarted.Equals(memObligation.DateStarted))
                {
                    switch (obligationFreq)
                    {
                        case Enums.ObligationFrequency.Monthly:

                            duration = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, memObligation.DateStarted, memberObligation.DateStarted);
                            break;

                        case Enums.ObligationFrequency.Yearly:

                            duration = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Year, memObligation.DateStarted, memberObligation.DateStarted);
                            break;
                    }

                    memberObligation.AdjustedBalance -= (memObligation.Amount * duration);
                }

                if (!memberObligation.CurrencyTypeID.Equals(memObligation.CurrencyTypeID))
                {
                    int svid = Convert.ToInt32(Enums.SystemVariable.LRDToUSDRate);
                    string exchangeRate = db.SystemVariables.Find(svid).Value;
                    string[] exchangeRateParts = exchangeRate.Split(':');
                    decimal usdRateToLrd = 0;
                    if (exchangeRateParts.Length.Equals(2))
                    {
                        usdRateToLrd = Convert.ToDecimal(exchangeRateParts[1]) / Convert.ToDecimal(exchangeRateParts[0]);
                    }

                    Enums.Currency currencyType = (Enums.Currency)memObligation.CurrencyTypeID;
                    switch (currencyType)
                    {
                        case Enums.Currency.LRD://changing from lrd to usd
                            memberObligation.Amount = memberObligation.Amount / usdRateToLrd;
                            memberObligation.AdjustedBalance = memberObligation.AdjustedBalance / usdRateToLrd;
                            break;
                        case Enums.Currency.USD://changing from usd to lrd
                            memberObligation.Amount = memberObligation.Amount * usdRateToLrd;
                            memberObligation.AdjustedBalance = memberObligation.AdjustedBalance * usdRateToLrd;
                            break;
                    }
                }

                duration = 0;
                obligationFreq = (Enums.ObligationFrequency)memberObligation.RecurranceTypeID;
                switch (obligationFreq)
                {
                    case Enums.ObligationFrequency.Monthly:

                        duration = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, memObligation.DateAdjusted, DateTime.Now);
                        break;

                    case Enums.ObligationFrequency.Yearly:

                        duration = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Year, memObligation.DateStarted, DateTime.Now);
                        break;
                }

                memberObligation.AdjustedBalance += (memObligation.Amount * duration);

                memberObligation.DateAdjusted = DateTime.Now;


                db.Entry(memberObligation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Members", new { id = Session["id"] });

                //if (this.MakeAdjustment(id, recurranceTypeID, modifiedCurrencyTypeID, modifiedAmount, modifiedDate))
                //{
                //    db.Entry(memberObligation).State = EntityState.Modified;
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}
            }

            ViewBag.CurrencyTypeID = new SelectList(db.CurrencyTypes, "ID", "CurrencyName", memberObligation.CurrencyTypeID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberObligation.MemberID);
            ViewBag.ObligationStatusTypeID = new SelectList(db.ObligationStatusTypes, "ID", "StatusName", memberObligation.ObligationStatusTypeID);
            ViewBag.ObligationTypeID = new SelectList(db.ObligationTypes, "ID", "ObligationName", memberObligation.ObligationTypeID);
            ViewBag.RecurranceTypeID = new SelectList(db.RecurranceTypes, "ID", "Recurrance", memberObligation.RecurranceTypeID);
            return View(memberObligation);
        }

        // GET: MemberObligations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberObligation memberObligation = db.MemberObligations.Find(id);
            if (memberObligation == null)
            {
                return HttpNotFound();
            }
            return View(memberObligation);
        }

        // POST: MemberObligations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberObligation memberObligation = db.MemberObligations.Find(id);
            db.MemberObligations.Remove(memberObligation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

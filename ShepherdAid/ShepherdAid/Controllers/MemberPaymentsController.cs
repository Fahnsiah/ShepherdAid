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
    public class MemberPaymentsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();

        public ActionResult ActiveObligations(int id)
        {
            var result = db.spMemberActiveObligations(id).ToList();
            Session["id"] = id;
            Member member = db.Members.Find(id);
            string fullName = string.Format("{0} {1} {2}", member.FirstName, member.MiddleName, member.LastName);
            ViewBag.FullName = fullName;

            return View(result);
        }
        // GET: MemberPayments
        public ActionResult Index()
        {
            var memberPayments = db.MemberPayments.Include(m => m.Member).Include(m => m.Payment);
            return View(memberPayments.ToList());
        }

        // GET: MemberPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberPayment memberPayment = db.MemberPayments.Find(id);
            if (memberPayment == null)
            {
                return HttpNotFound();
            }
            return View(memberPayment);
        }

        // GET: MemberPayments/Create
        public ActionResult Create(int id)
        {
            TempData["obligation_id"] = id;
            int memberID = Convert.ToInt32(Session["id"]);
            Member member = db.Members.Find(memberID);
            ViewBag.Title = string.Format("Payment for {0}, {1} {2} ({3})", member.LastName, member.FirstName, member.MiddleName, member.MemberID);
            var result = db.spObligationSummary(id).SingleOrDefault();

            decimal accumulated = 0, totalPayment = 0, currentBalance = 0;
            if (result.AccumulatedAmount != null)
            {
                accumulated = Convert.ToDecimal(result.AccumulatedAmount);
                totalPayment = Convert.ToDecimal(result.TotalPayment);
                currentBalance = Convert.ToDecimal(result.CurrentBalance);
            }

            ViewBag.Accumulated = string.Format("{0} {1}", accumulated.ToString("C"), result.Currency);
            ViewBag.TotalPayment = string.Format("{0} {1}", totalPayment.ToString("C"), result.Currency);
            ViewBag.CurrentBalance = string.Format("{0} {1}", currentBalance.ToString("C"), result.Currency);


            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberID);
            return View();
        }

        // POST: MemberPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Remark")] MemberPayment memberPayment, decimal AmountPaid)
        {
            //int id = 0;
            Payment payment = new Payment();

            try
            {
                int obligationID = Convert.ToInt32(TempData["obligation_id"]);
                int currencyTypeID = db.MemberObligations.Find(obligationID).CurrencyTypeID;

                //add the payment record
                payment.AmountPaid = AmountPaid;
                payment.CurrencyTypeID = currencyTypeID;
                payment.PaymentDate = DateTime.Now;
                payment.RecordedBy = User.Identity.GetUserName();
                payment.DateRecorded = DateTime.Now;

                db.Payments.Add(payment);
                db.SaveChanges();

                if (ModelState.IsValid)
                {
                    int memberID = Convert.ToInt32(Session["id"]);
                    memberPayment.PaymentID = payment.ID;
                    memberPayment.MemberID = memberID;
                    memberPayment.MemberObligationID = obligationID;
                    memberPayment.RecordedBy = User.Identity.GetUserName();
                    memberPayment.DateRecorded = DateTime.Now;
                    db.MemberPayments.Add(memberPayment);
                    db.SaveChanges();
                    return RedirectToAction("ActiveObligations", "MemberPayments", new { id = memberID });
                }

            }
            catch (Exception ex)
            {
                //Payment payment = db.Payments.Find(id);
                db.Payments.Remove(payment);
                db.SaveChanges();

                ModelState.AddModelError("Create Error", ex.Message);
            }

            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberPayment.MemberID);
            ViewBag.PaymentID = new SelectList(db.Payments, "ID", "RecordedBy", memberPayment.PaymentID);
            return View(memberPayment);
        }

        // GET: MemberPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberPayment memberPayment = db.MemberPayments.Find(id);
            if (memberPayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberPayment.MemberID);
            //ViewBag.MemberObligationID = new SelectList(db.MembersObligations, "ID", "RecordedBy", memberPayment.MemberObligationID);
            ViewBag.PaymentID = new SelectList(db.Payments, "ID", "RecordedBy", memberPayment.PaymentID);
            return View(memberPayment);
        }

        // POST: MemberPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MemberID,PaymentID,MemberObligationID,Remark,RecordedBy,DateRecorded")] MemberPayment memberPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberID", memberPayment.MemberID);
            //ViewBag.MemberObligationID = new SelectList(db.MembersObligations, "ID", "RecordedBy", memberPayment.MemberObligationID);
            ViewBag.PaymentID = new SelectList(db.Payments, "ID", "RecordedBy", memberPayment.PaymentID);
            return View(memberPayment);
        }

        // GET: MemberPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberPayment memberPayment = db.MemberPayments.Find(id);
            if (memberPayment == null)
            {
                return HttpNotFound();
            }
            return View(memberPayment);
        }

        // POST: MemberPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberPayment memberPayment = db.MemberPayments.Find(id);
            db.MemberPayments.Remove(memberPayment);
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

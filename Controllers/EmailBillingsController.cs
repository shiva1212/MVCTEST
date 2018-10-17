using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models;
using PagedList;

namespace MvcTest.Controllers
{
    public class EmailBillingsController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: EmailBillings
        public ActionResult Index(string SearchString,string CurrentFilter,int?Page,string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortClientCodeparameter = string.IsNullOrEmpty(sortBy) ? "ClientCode desc" : "";
            ViewBag.SortDateparameter = sortBy == "Date" ? "Date desc" : "Date";
            if(SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var emailBillings = db.EmailBillings.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                emailBillings = emailBillings.Where(x => x.ClientCode.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "ClientCode desc":
                    emailBillings = emailBillings.OrderByDescending(x => x.ClientCode);
                    break;
                case "Date desc":
                    emailBillings = emailBillings.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    emailBillings = emailBillings.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    emailBillings = emailBillings.OrderBy(x => x.ClientCode);
                    break;

                   
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(emailBillings.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: EmailBillings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailBilling emailBilling = db.EmailBillings.FirstOrDefault(m => m.ReferenceNo == id);
            if (emailBilling == null)
            {
                return HttpNotFound();
            }
            return View(emailBilling);
        }

        // GET: EmailBillings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailBillings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReferenceNo,ClientCode,Status,Type,Description,FromEmail,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] EmailBilling emailBilling)
        {
            if (ModelState.IsValid)
            {
                db.EmailBillings.Add(emailBilling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emailBilling);
        }

        // GET: EmailBillings/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailBilling emailBilling = db.EmailBillings.FirstOrDefault(m => m.ReferenceNo == id);
            if (emailBilling == null)
            {
                return HttpNotFound();
            }
            return View(emailBilling);
        }

        // POST: EmailBillings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReferenceNo,ClientCode,Status,Type,Description,FromEmail,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] EmailBilling emailBilling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailBilling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emailBilling);
        }

        // GET: EmailBillings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailBilling emailBilling = db.EmailBillings.FirstOrDefault(m => m.ReferenceNo == id);
            if (emailBilling == null)
            {
                return HttpNotFound();
            }
            return View(emailBilling);
        }

        // POST: EmailBillings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EmailBilling emailBilling = db.EmailBillings.Find(id);
            db.EmailBillings.Remove(emailBilling);
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

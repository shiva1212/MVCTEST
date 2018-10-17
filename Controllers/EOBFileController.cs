using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcTest.Models;

namespace mvcTest.Controllers
{
   
    public class EOBFileController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: EOBFile

        public ActionResult Index(int? Page, string SearchString, string CurrentFilter, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortClientCodeParameter = string.IsNullOrEmpty(sortBy) ? "ClientCode desc" : "";
            ViewBag.SortCreatedDateParameter = sortBy == "CreatedDate" ? "CreatedDate desc" : "CreatedDate";
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var bill_Det_EOBFile = db.Bill_Det_EOBFile.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                bill_Det_EOBFile = bill_Det_EOBFile.Include(b => b.Bill_Mst_1500Form);
                bill_Det_EOBFile = bill_Det_EOBFile.Where(s => s.BillNo.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "ClientCode desc":
                    bill_Det_EOBFile = bill_Det_EOBFile.OrderByDescending(x => x.ClientCode);
                    break;
                case "CreatedDate desc":
                    bill_Det_EOBFile = bill_Det_EOBFile.OrderByDescending(x => x.CreatedDate);
                    break;
                case "CreatedDate":
                    bill_Det_EOBFile = bill_Det_EOBFile.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    bill_Det_EOBFile = bill_Det_EOBFile.OrderBy(x => x.ClientCode);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(bill_Det_EOBFile.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: EOBFile/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_Det_EOBFile bill_Det_EOBFile = db.Bill_Det_EOBFile.SingleOrDefault(m => m.RowNum == id);
            if (bill_Det_EOBFile == null)
            {
                return HttpNotFound();
            }
            return View(bill_Det_EOBFile);
        }

        // GET: EOBFile/Create
        public ActionResult Create()
        {
            ViewBag.ReferenceNo = new SelectList(db.Bill_Mst_1500Form, "ReferenceNo", "ClientCode");
            return View();
        }

        // POST: EOBFile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RowNum,ReferenceNo,ClientCode,BillNo,BillPostId,PostingDate,ChequeNo,Amount,CreatedDate,CreatedBy,EOBFileName,LocalEOBFile,EOBPageNO")] Bill_Det_EOBFile bill_Det_EOBFile)
        {
            if (ModelState.IsValid)
            {
                db.Bill_Det_EOBFile.Add(bill_Det_EOBFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReferenceNo = new SelectList(db.Bill_Mst_1500Form, "ReferenceNo", "ClientCode", bill_Det_EOBFile.ReferenceNo);
            return View(bill_Det_EOBFile);
        }

        // GET: EOBFile/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_Det_EOBFile bill_Det_EOBFile = db.Bill_Det_EOBFile.SingleOrDefault(m => m.RowNum == id);
            if (bill_Det_EOBFile == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReferenceNo = new SelectList(db.Bill_Mst_1500Form, "ReferenceNo", "ClientCode", bill_Det_EOBFile.ReferenceNo);
            return View(bill_Det_EOBFile);
        }

        // POST: EOBFile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RowNum,ReferenceNo,ClientCode,BillNo,BillPostId,PostingDate,ChequeNo,Amount,CreatedDate,CreatedBy,EOBFileName,LocalEOBFile,EOBPageNO")] Bill_Det_EOBFile bill_Det_EOBFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill_Det_EOBFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReferenceNo = new SelectList(db.Bill_Mst_1500Form, "ReferenceNo", "ClientCode", bill_Det_EOBFile.ReferenceNo);
            return View(bill_Det_EOBFile);
        }

        // GET: EOBFile/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_Det_EOBFile bill_Det_EOBFile = db.Bill_Det_EOBFile.SingleOrDefault(m => m.RowNum == id);
            if (bill_Det_EOBFile == null)
            {
                return HttpNotFound();
            }
            return View(bill_Det_EOBFile);
        }

        // POST: EOBFile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Bill_Det_EOBFile bill_Det_EOBFile = db.Bill_Det_EOBFile.Find(id);
            db.Bill_Det_EOBFile.Remove(bill_Det_EOBFile);
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

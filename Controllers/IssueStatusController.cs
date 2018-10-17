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
using PagedList.Mvc;


namespace Katsi.Controllers
{
    
    public class IssueStatusController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Gen_Mst_IssueStatus

        public ActionResult Index(string SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortIssueStatusParameter = string.IsNullOrEmpty(sortBy) ? "Issue Status desc" : "";
            ViewBag.SortDateParameter = sortBy == "Date" ? "Date desc" : "Date";
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var gen_Mst_IssueStatus = db.Gen_Mst_IssueStatus.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_IssueStatus = gen_Mst_IssueStatus.Where(x => x.IssueStatus.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "Issue Status desc":
                    gen_Mst_IssueStatus = gen_Mst_IssueStatus.OrderByDescending(x => x.IssueStatus);
                    break;
                case "Date desc":
                    gen_Mst_IssueStatus = gen_Mst_IssueStatus.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_IssueStatus = gen_Mst_IssueStatus.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_IssueStatus = gen_Mst_IssueStatus.OrderBy(x => x.IssueStatus);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_IssueStatus.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: Gen_Mst_IssueStatus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_IssueStatus gen_Mst_IssueStatus = db.Gen_Mst_IssueStatus.FirstOrDefault(m => m.StatusCode == id);
            if (gen_Mst_IssueStatus == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_IssueStatus);
        }

        // GET: Gen_Mst_IssueStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gen_Mst_IssueStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusCode,IssueStatus,CreatedBy,CreatedDate")] Gen_Mst_IssueStatus gen_Mst_IssueStatus)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_IssueStatus.Add(gen_Mst_IssueStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gen_Mst_IssueStatus);
        }

        // GET: Gen_Mst_IssueStatus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_IssueStatus gen_Mst_IssueStatus = db.Gen_Mst_IssueStatus.FirstOrDefault(m => m.StatusCode == id);
            if (gen_Mst_IssueStatus == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_IssueStatus);
        }

        // POST: Gen_Mst_IssueStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusCode,IssueStatus,CreatedBy,CreatedDate")] Gen_Mst_IssueStatus gen_Mst_IssueStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_IssueStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gen_Mst_IssueStatus);
        }

        // GET: Gen_Mst_IssueStatus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_IssueStatus gen_Mst_IssueStatus = db.Gen_Mst_IssueStatus.FirstOrDefault(m => m.StatusCode == id);
            if (gen_Mst_IssueStatus == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_IssueStatus);
        }

        // POST: Gen_Mst_IssueStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_IssueStatus gen_Mst_IssueStatus = db.Gen_Mst_IssueStatus.FirstOrDefault(m => m.StatusCode == id);
            db.Gen_Mst_IssueStatus.Remove(gen_Mst_IssueStatus);
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

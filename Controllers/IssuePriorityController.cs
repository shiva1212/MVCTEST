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
    
    public class IssuePriorityController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Gen_Mst_IssuePriority

        public ActionResult Index(string SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortIssuePriorityParameter = string.IsNullOrEmpty(sortBy) ? "IssuePriority desc" : "";
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
            var gen_Mst_IssuePriority = db.Gen_Mst_IssuePriority.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_IssuePriority = gen_Mst_IssuePriority.Where(x => x.IssuePriority.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "IssuePriority desc":
                    gen_Mst_IssuePriority = gen_Mst_IssuePriority.OrderByDescending(x => x.IssuePriority);
                    break;
                case "Date desc":
                    gen_Mst_IssuePriority = gen_Mst_IssuePriority.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_IssuePriority = gen_Mst_IssuePriority.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_IssuePriority = gen_Mst_IssuePriority.OrderBy(x => x.IssuePriority);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_IssuePriority.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: Gen_Mst_IssuePriority/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_IssuePriority gen_Mst_IssuePriority = db.Gen_Mst_IssuePriority.FirstOrDefault(m => m.PriorityCode == id);
            if (gen_Mst_IssuePriority == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_IssuePriority);
        }

        // GET: Gen_Mst_IssuePriority/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gen_Mst_IssuePriority/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriorityCode,IssuePriority,CreatedBy,CreatedDate")] Gen_Mst_IssuePriority gen_Mst_IssuePriority)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_IssuePriority.Add(gen_Mst_IssuePriority);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gen_Mst_IssuePriority);
        }

        // GET: Gen_Mst_IssuePriority/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_IssuePriority gen_Mst_IssuePriority = db.Gen_Mst_IssuePriority.FirstOrDefault(m => m.PriorityCode == id);
            if (gen_Mst_IssuePriority == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_IssuePriority);
        }

        // POST: Gen_Mst_IssuePriority/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriorityCode,IssuePriority,CreatedBy,CreatedDate")] Gen_Mst_IssuePriority gen_Mst_IssuePriority)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_IssuePriority).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gen_Mst_IssuePriority);
        }

        // GET: Gen_Mst_IssuePriority/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_IssuePriority gen_Mst_IssuePriority = db.Gen_Mst_IssuePriority.FirstOrDefault(m => m.PriorityCode == id);
            if (gen_Mst_IssuePriority == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_IssuePriority);
        }

        // POST: Gen_Mst_IssuePriority/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_IssuePriority gen_Mst_IssuePriority = db.Gen_Mst_IssuePriority.FirstOrDefault(m => m.PriorityCode == id);
            db.Gen_Mst_IssuePriority.Remove(gen_Mst_IssuePriority);
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

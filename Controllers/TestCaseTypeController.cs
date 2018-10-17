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
  
    public class TestCaseTypeController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Gen_Mst_TestCaseType

        public ActionResult Index(string SearchingString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortDescriptionParameter = string.IsNullOrEmpty(sortBy) ? "Description desc" : "";
            if (SearchingString != null)
            {
                Page = 1;
            }
            else
            {
                SearchingString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchingString;
            var gen_Mst_TestCaseType = db.Gen_Mst_TestCaseType.AsQueryable();

            if (!String.IsNullOrEmpty(SearchingString))
            {
                gen_Mst_TestCaseType = gen_Mst_TestCaseType.Include(g => g.Rights_MST_UserMaster).Include(g => g.Rights_MST_UserMaster1);
                return View(gen_Mst_TestCaseType.Where(x => x.TestCaseCode.Contains(SearchingString)));
            }
            switch (sortBy)
            {
                case "Description desc":
                    gen_Mst_TestCaseType = gen_Mst_TestCaseType.OrderByDescending(x => x.Description);
                    break;
                default:
                    gen_Mst_TestCaseType = gen_Mst_TestCaseType.OrderBy(x => x.Description);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_TestCaseType.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: Gen_Mst_TestCaseType/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_TestCaseType gen_Mst_TestCaseType = db.Gen_Mst_TestCaseType.FirstOrDefault(m => m.TestCaseCode == id);
            if (gen_Mst_TestCaseType == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_TestCaseType);
        }

        // GET: Gen_Mst_TestCaseType/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            return View();
        }

        // POST: Gen_Mst_TestCaseType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestCaseCode,Description,Comments,CreatedBy,CreatedDate")] Gen_Mst_TestCaseType gen_Mst_TestCaseType)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_TestCaseType.Add(gen_Mst_TestCaseType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_TestCaseType.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_TestCaseType.ModifiedBy);
            return View(gen_Mst_TestCaseType);
        }

        // GET: Gen_Mst_TestCaseType/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_TestCaseType gen_Mst_TestCaseType = db.Gen_Mst_TestCaseType.FirstOrDefault(m => m.TestCaseCode == id);
            if (gen_Mst_TestCaseType == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_TestCaseType.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_TestCaseType.ModifiedBy);
            return View(gen_Mst_TestCaseType);
        }

        // POST: Gen_Mst_TestCaseType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestCaseCode,Description,Comments,CreatedBy,CreatedDate")] Gen_Mst_TestCaseType gen_Mst_TestCaseType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_TestCaseType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_TestCaseType.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_TestCaseType.ModifiedBy);
            return View(gen_Mst_TestCaseType);
        }

        // GET: Gen_Mst_TestCaseType/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_TestCaseType gen_Mst_TestCaseType = db.Gen_Mst_TestCaseType.FirstOrDefault(m => m.TestCaseCode == id);
            if (gen_Mst_TestCaseType == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_TestCaseType);
        }

        // POST: Gen_Mst_TestCaseType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_TestCaseType gen_Mst_TestCaseType = db.Gen_Mst_TestCaseType.FirstOrDefault(m => m.TestCaseCode == id);
            db.Gen_Mst_TestCaseType.Remove(gen_Mst_TestCaseType);
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

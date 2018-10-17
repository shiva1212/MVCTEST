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
    
    public class GeneralTestCasesController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Gen_Mst_GeneralTestCases

        public ActionResult Index(string SearchingString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortStandardNameParameter = string.IsNullOrEmpty(sortBy) ? "StandardName desc" : "";
            ViewBag.SortDateParameter = sortBy == "Date" ? "Date desc" : "Date";
            if (SearchingString != null)
            {
                Page = 1;
            }
            else
            {
                SearchingString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchingString;
            var gen_Mst_GeneralTestCases = db.Gen_Mst_GeneralTestCases.AsQueryable();
            if (!String.IsNullOrEmpty(SearchingString))
            {
                gen_Mst_GeneralTestCases = gen_Mst_GeneralTestCases.Include(g => g.Gen_Mst_TestCaseType).Include(g => g.Rights_MST_UserMaster).Include(g => g.Rights_MST_UserMaster1);
                gen_Mst_GeneralTestCases = gen_Mst_GeneralTestCases.Where(x => x.StandardName.Contains(SearchingString));
            }
            switch (sortBy)
            {
                case "StandardName desc":
                    gen_Mst_GeneralTestCases = gen_Mst_GeneralTestCases.OrderByDescending(x => x.StandardName);
                    break;
                case "Date desc":
                    gen_Mst_GeneralTestCases = gen_Mst_GeneralTestCases.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_GeneralTestCases = gen_Mst_GeneralTestCases.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_GeneralTestCases = gen_Mst_GeneralTestCases.OrderBy(x => x.StandardName);
                    break;

            }

            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_GeneralTestCases.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: Gen_Mst_GeneralTestCases/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_GeneralTestCases gen_Mst_GeneralTestCases = db.Gen_Mst_GeneralTestCases.FirstOrDefault(m => m.TestCaseID == id);
            if (gen_Mst_GeneralTestCases == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_GeneralTestCases);
        }

        // GET: Gen_Mst_GeneralTestCases/Create
        public ActionResult Create()
        {
            ViewBag.TestCaseCode = new SelectList(db.Gen_Mst_TestCaseType, "TestCaseCode", "Description");
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            return View();
        }

        // POST: Gen_Mst_GeneralTestCases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestCaseID,TestCaseCode,StandardName,StandardDesc,Comments,TestCaseOrder,CreatedDate,CreatedBy")] Gen_Mst_GeneralTestCases gen_Mst_GeneralTestCases)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_GeneralTestCases.Add(gen_Mst_GeneralTestCases);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestCaseCode = new SelectList(db.Gen_Mst_TestCaseType, "TestCaseCode", "Description", gen_Mst_GeneralTestCases.TestCaseCode);
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_GeneralTestCases.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_GeneralTestCases.ModifiedBy);
            return View(gen_Mst_GeneralTestCases);
        }

        // GET: Gen_Mst_GeneralTestCases/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_GeneralTestCases gen_Mst_GeneralTestCases = db.Gen_Mst_GeneralTestCases.FirstOrDefault(m => m.TestCaseID == id);
            if (gen_Mst_GeneralTestCases == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestCaseCode = new SelectList(db.Gen_Mst_TestCaseType, "TestCaseCode", "Description", gen_Mst_GeneralTestCases.TestCaseCode);
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_GeneralTestCases.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_GeneralTestCases.ModifiedBy);
            return View(gen_Mst_GeneralTestCases);
        }

        // POST: Gen_Mst_GeneralTestCases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestCaseID,TestCaseCode,StandardName,StandardDesc,Comments,TestCaseOrder,CreatedDate,CreatedBy")] Gen_Mst_GeneralTestCases gen_Mst_GeneralTestCases)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_GeneralTestCases).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestCaseCode = new SelectList(db.Gen_Mst_TestCaseType, "TestCaseCode", "Description", gen_Mst_GeneralTestCases.TestCaseCode);
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_GeneralTestCases.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_GeneralTestCases.ModifiedBy);
            return View(gen_Mst_GeneralTestCases);
        }

        // GET: Gen_Mst_GeneralTestCases/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_GeneralTestCases gen_Mst_GeneralTestCases = db.Gen_Mst_GeneralTestCases.FirstOrDefault(m => m.TestCaseID == id);
            if (gen_Mst_GeneralTestCases == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_GeneralTestCases);
        }

        // POST: Gen_Mst_GeneralTestCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Gen_Mst_GeneralTestCases gen_Mst_GeneralTestCases = db.Gen_Mst_GeneralTestCases.FirstOrDefault(m => m.TestCaseID == id);
            db.Gen_Mst_GeneralTestCases.Remove(gen_Mst_GeneralTestCases);
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

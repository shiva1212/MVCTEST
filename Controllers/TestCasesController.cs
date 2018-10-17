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
  
    public class TestCasesController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: TestCases

        public ActionResult Index(string SearchingString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortComponentsCodeParameter = string.IsNullOrEmpty(sortBy) ? "ComponentsCode desc" : "";
            ViewBag.SortFunctionNameParameter = sortBy == "FunctionName" ? "FunctionName desc" : "FunctionName";
            if (SearchingString != null)
            {
                Page = 1;
            }
            else
            {
                SearchingString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchingString;
            var gen_Mst_TestCases = db.Gen_Mst_TestCases.AsQueryable();
            if (!String.IsNullOrEmpty(SearchingString))
            {
                gen_Mst_TestCases = gen_Mst_TestCases.Where(s => s.TabPagecode.Contains(SearchingString));
            }
            switch (sortBy)
            {
                case "ComponentsCode desc":
                    gen_Mst_TestCases = gen_Mst_TestCases.OrderByDescending(x => x.ComponentsCode);
                    break;
                case "FunctionName desc":
                    gen_Mst_TestCases = gen_Mst_TestCases.OrderByDescending(x => x.FunctionName);
                    break;
                case "FunctionName":
                    gen_Mst_TestCases = gen_Mst_TestCases.OrderBy(x => x.FunctionName);
                    break;
                default:
                    gen_Mst_TestCases = gen_Mst_TestCases.OrderBy(x => x.ComponentsCode);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_TestCases.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: TestCases/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_TestCases gen_Mst_TestCases = db.Gen_Mst_TestCases.FirstOrDefault(m => m.TestCaseID == id);
            if (gen_Mst_TestCases == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_TestCases);
        }

        // GET: TestCases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestCases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestCaseID,ProductCode,ModuleCode,ScreenCode,TabPagecode,ComponentsCode,FunctionName,Scenario,ExpectedResults,ReferenceTables,Comments,TestCaseOrder,CreatedDate,CreatedBy,Objective,DataInputs,ActualResults")] Gen_Mst_TestCases gen_Mst_TestCases)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_TestCases.Add(gen_Mst_TestCases);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gen_Mst_TestCases);
        }

        // GET: TestCases/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_TestCases gen_Mst_TestCases = db.Gen_Mst_TestCases.FirstOrDefault(m => m.TestCaseID == id);
            if (gen_Mst_TestCases == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_TestCases);
        }

        // POST: TestCases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestCaseID,ProductCode,ModuleCode,ScreenCode,TabPagecode,ComponentsCode,FunctionName,Scenario,ExpectedResults,ReferenceTables,Comments,TestCaseOrder,CreatedDate,CreatedBy,Objective,DataInputs,ActualResults")] Gen_Mst_TestCases gen_Mst_TestCases)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_TestCases).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gen_Mst_TestCases);
        }

        // GET: TestCases/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_TestCases gen_Mst_TestCases = db.Gen_Mst_TestCases.FirstOrDefault(m => m.TestCaseID == id);
            if (gen_Mst_TestCases == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_TestCases);
        }

        // POST: TestCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Gen_Mst_TestCases gen_Mst_TestCases = db.Gen_Mst_TestCases.FirstOrDefault(m => m.TestCaseID == id);
            db.Gen_Mst_TestCases.Remove(gen_Mst_TestCases);
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

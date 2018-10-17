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
using System.Data.SqlClient;

namespace mvcTest.Controllers
{
   
    public class ModuleController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Gen_Mst_Module

        public ActionResult Index(string SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortModuleNameParameter = string.IsNullOrEmpty(sortBy) ? "ModuleName desc" : "";
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
            var gen_Mst_Module = db.Gen_Mst_Module.AsQueryable();

            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_Module = gen_Mst_Module.Include(g => g.Gen_Mst_Product);
                gen_Mst_Module = gen_Mst_Module.Where(s => s.ModuleName.Contains(SearchString) || s.Comments.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "ModuleName desc":
                    gen_Mst_Module = gen_Mst_Module.OrderByDescending(x => x.ModuleName);
                    break;
                case "Date desc":
                    gen_Mst_Module = gen_Mst_Module.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_Module = gen_Mst_Module.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_Module = gen_Mst_Module.OrderBy(x => x.ModuleName);
                    break;

            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_Module.ToList().ToPagedList(PageNumber, PageSize));

        }
        //[HttpPost]
        //public JsonResult Index(string search)
        //{
        //    KATSI_PRVEntities db = new KATSI_PRVEntities();
        //    List<Gen_Mst_Module> allsearch = db.
        //}

        // GET: Gen_Mst_Module/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Module gen_Mst_Module = db.Gen_Mst_Module.FirstOrDefault(m => m.ModuleCode == id);
            if (gen_Mst_Module == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Module);
        }

        // GET: Gen_Mst_Module/Create
        public ActionResult Create()
        {
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName");
            return View();
        }

        // POST: Gen_Mst_Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ModuleCode,ModuleName,Comments,ModuleOrder,CreatedBy,CreatedDate")] Gen_Mst_Module gen_Mst_Module)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_Module.Add(gen_Mst_Module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Module.ProductCode);
            return View(gen_Mst_Module);
        }

        // GET: Gen_Mst_Module/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Module gen_Mst_Module = db.Gen_Mst_Module.FirstOrDefault(m => m.ModuleCode == id);
            if (gen_Mst_Module == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Module.ProductCode);
            return View(gen_Mst_Module);
        }

        // POST: Gen_Mst_Module/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,ModuleCode,ModuleName,Comments,ModuleOrder,CreatedBy,CreatedDate")] Gen_Mst_Module gen_Mst_Module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_Module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Module.ProductCode);
            return View(gen_Mst_Module);
        }

        // GET: Gen_Mst_Module/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Module gen_Mst_Module = db.Gen_Mst_Module.FirstOrDefault(m => m.ModuleCode == id);
            if (gen_Mst_Module == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Module);
        }

        // POST: Gen_Mst_Module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_Module gen_Mst_Module = db.Gen_Mst_Module.FirstOrDefault(m => m.ModuleCode == id);
            db.Gen_Mst_Module.Remove(gen_Mst_Module);
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

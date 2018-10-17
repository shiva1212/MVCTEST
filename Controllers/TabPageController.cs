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

namespace mvcTest.Controllers
{
    
    public class TabPageController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: TabPage

        public ActionResult Index(String SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortTabPageNameParameter = string.IsNullOrEmpty(sortBy) ? "TabPageName desc" : "";
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
            var gen_Mst_TabPage = db.Gen_Mst_TabPage.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_TabPage = gen_Mst_TabPage.Include(g => g.Gen_Mst_Screen);
                gen_Mst_TabPage = gen_Mst_TabPage.Where(s => s.TabPageName.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "TabPageName desc":
                    gen_Mst_TabPage = gen_Mst_TabPage.OrderByDescending(x => x.TabPageName);
                    break;
                case "Date desc":
                    gen_Mst_TabPage = gen_Mst_TabPage.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_TabPage = gen_Mst_TabPage.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_TabPage = gen_Mst_TabPage.OrderBy(x => x.TabPageName);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_TabPage.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: TabPage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_TabPage gen_Mst_TabPage = db.Gen_Mst_TabPage.FirstOrDefault(m => m.RowNum == id);
            if (gen_Mst_TabPage == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_TabPage);
        }

        // GET: TabPage/Create
        public ActionResult Create()
        {
            ViewBag.ScreenCode = new SelectList(db.Gen_Mst_Screen, "ScreenCode", "ScreenName");
            ViewBag.ModuleCode = new SelectList(db.Gen_Mst_Module, "ModuleCode", "ModuleName");
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName");
            return View();
        }

        // POST: TabPage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ModuleCode,ScreenCode,TabPagecode,TabPageName,Comments,TabPageOrder,CreatedBy,CreatedDate,RowNum")] Gen_Mst_TabPage gen_Mst_TabPage)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_TabPage.Add(gen_Mst_TabPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScreenCode = new SelectList(db.Gen_Mst_Screen, "ScreenCode", "ScreenName", gen_Mst_TabPage.ScreenCode);
            ViewBag.ModuleCode = new SelectList(db.Gen_Mst_Module, "ModuleCode", "ModuleName", gen_Mst_TabPage.ModuleCode);
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_TabPage.ProductCode);
            return View(gen_Mst_TabPage);
        }

        // GET: TabPage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_TabPage gen_Mst_TabPage = db.Gen_Mst_TabPage.FirstOrDefault(m => m.RowNum == id);
            if (gen_Mst_TabPage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScreenCode = new SelectList(db.Gen_Mst_Screen, "ScreenCode", "ScreenName",gen_Mst_TabPage.ScreenCode);
            ViewBag.ModuleCode = new SelectList(db.Gen_Mst_Module, "ModuleCode", "ModuleCode", gen_Mst_TabPage.ModuleCode);
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_TabPage.ProductCode);

            //ViewBag.ProductCode = new SelectList(db.Gen_Mst_Screen, "ProductCode", "ScreenName", gen_Mst_TabPage.ProductCode);
            return View(gen_Mst_TabPage);
        }

        // POST: TabPage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,ModuleCode,ScreenCode,TabPagecode,TabPageName,Comments,TabPageOrder,CreatedBy,CreatedDate,RowNum")] Gen_Mst_TabPage gen_Mst_TabPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_TabPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Screen, "ProductCode", "ScreenName", gen_Mst_TabPage.ProductCode);
            return View(gen_Mst_TabPage);
        }

        // GET: TabPage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_TabPage gen_Mst_TabPage = db.Gen_Mst_TabPage.FirstOrDefault(m => m.RowNum == id);
            if (gen_Mst_TabPage == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_TabPage);
        }

        // POST: TabPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Gen_Mst_TabPage gen_Mst_TabPage = db.Gen_Mst_TabPage.FirstOrDefault(m => m.RowNum == id);
            db.Gen_Mst_TabPage.Remove(gen_Mst_TabPage);
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

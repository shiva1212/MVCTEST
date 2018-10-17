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

    public class ComponentsController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Components

        public ActionResult Index(string SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortComponentsNameParameter = string.IsNullOrEmpty(sortBy) ? "ComponentsName desc" : "";
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
            var gen_Mst_Components = db.Gen_Mst_Components.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_Components = gen_Mst_Components.Include(g => g.Gen_Mst_Screen);
                gen_Mst_Components = gen_Mst_Components.Where(s => s.ComponentsName.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "ComponentsName desc":
                    gen_Mst_Components = gen_Mst_Components.OrderByDescending(x => x.ComponentsName);
                    break;
                case "Date desc":
                    gen_Mst_Components = gen_Mst_Components.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_Components = gen_Mst_Components.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_Components = gen_Mst_Components.OrderBy(x => x.ComponentsName);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_Components.ToList().ToPagedList(PageNumber, PageSize));


        }

        // GET: Components/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Components gen_Mst_Components = db.Gen_Mst_Components.FirstOrDefault(m => m.RowNum == id);
            if (gen_Mst_Components == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Components);
        }

        // GET: Components/Create
        public ActionResult Create()
        {
            ViewBag.TabPageCode = new SelectList(db.Gen_Mst_TabPage, "TabPageCode", "TabPageName");
            ViewBag.ScreenCode = new SelectList(db.Gen_Mst_Screen, "ScreenCode", "ScreenName");
            ViewBag.ModuleCode = new SelectList(db.Gen_Mst_Module, "ModuleCode", "ModuleName");
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName");
            return View();
        }

        // POST: Components/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ModuleCode,ScreenCode,Componentscode,ComponentsName,TabPageCode,Comments,ComponentsOrder,CreatedBy,CreatedDate,RowNum,Description,DataComments,Datatype,Length,Tablecode,FieldName,Mandatory,IsDeleted")] Gen_Mst_Components gen_Mst_Components)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_Components.Add(gen_Mst_Components);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TabPageCode = new SelectList(db.Gen_Mst_TabPage, "TabPageCode", "TabPageName", gen_Mst_Components.TabPageCode);
            ViewBag.ScreenCode = new SelectList(db.Gen_Mst_Screen, "ScreenCode", "ScreenName", gen_Mst_Components.ScreenCode);
            ViewBag.ModuleCode = new SelectList(db.Gen_Mst_Module, "ModuleCode", "ModuleName", gen_Mst_Components.ModuleCode);
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Components.ProductCode);
            return View(gen_Mst_Components);
        }

        // GET: Components/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Components gen_Mst_Components = db.Gen_Mst_Components.FirstOrDefault(m => m.RowNum == id);
            if (gen_Mst_Components == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Screen, "ProductCode", "ScreenName", gen_Mst_Components.ProductCode);
            return View(gen_Mst_Components);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,ModuleCode,ScreenCode,Componentscode,ComponentsName,TabPageCode,Comments,ComponentsOrder,CreatedBy,CreatedDate,RowNum,Description,DataComments,Datatype,Length,Tablecode,FieldName,Mandatory,IsDeleted")] Gen_Mst_Components gen_Mst_Components)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_Components).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Screen, "ProductCode", "ScreenName", gen_Mst_Components.ProductCode);
            return View(gen_Mst_Components);
        }

        // GET: Components/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Components gen_Mst_Components = db.Gen_Mst_Components.FirstOrDefault(m => m.RowNum == id);
            if (gen_Mst_Components == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Components);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Gen_Mst_Components gen_Mst_Components = db.Gen_Mst_Components.FirstOrDefault(m => m.RowNum == id);
            db.Gen_Mst_Components.Remove(gen_Mst_Components);
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

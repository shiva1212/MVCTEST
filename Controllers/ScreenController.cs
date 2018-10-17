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
   
    public class ScreenController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Gen_Mst_Screen

        public ActionResult Index(String SearchString, string currentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortCommentsParameter = string.IsNullOrEmpty(sortBy) ? "Comments desc" : "";
            ViewBag.SortScreenNameParameter = sortBy == "ScreenName" ? "ScreenName desc" : "ScreenName";
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.currentFilter = SearchString;
            var gen_Mst_Screen = db.Gen_Mst_Screen.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                //var gen_Mst_Screen = db.Gen_Mst_Screen.Include(g => g.Gen_Mst_Module);
                gen_Mst_Screen = gen_Mst_Screen.Include(g => g.Gen_Mst_Module);
                gen_Mst_Screen = gen_Mst_Screen.Where(s => s.ScreenName.Contains(SearchString) || SearchString == null);
            }

            switch (sortBy)
            {
                case "Comments desc":
                    gen_Mst_Screen = gen_Mst_Screen.OrderByDescending(x => x.Comments);
                    break;
                case "ScreeName desc":
                    gen_Mst_Screen = gen_Mst_Screen.OrderByDescending(x => x.ScreenName);
                    break;
                case "ScreenName":
                    gen_Mst_Screen = gen_Mst_Screen.OrderBy(x => x.ScreenName);
                    break;
                default:
                    gen_Mst_Screen = gen_Mst_Screen.OrderBy(x => x.Comments);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_Screen.ToPagedList(PageNumber, PageSize));
        }

        // GET: Gen_Mst_Screen/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Screen gen_Mst_Screen = db.Gen_Mst_Screen.FirstOrDefault(m => m.ScreenName == id);
            if (gen_Mst_Screen == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Screen);
        }

        // GET: Gen_Mst_Screen/Create
        public ActionResult Create()
        {
            ViewBag.ModuleCode = new SelectList(db.Gen_Mst_Module, "ModuleCode", "ModuleName");
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName");
            return View();
        }

        // POST: Gen_Mst_Screen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ModuleCode,ScreenCode,ScreenName,Comments,ScreenOrder,CreatedBy,CreatedDate,RowNum")] Gen_Mst_Screen gen_Mst_Screen)
        {
            if (ModelState.IsValid)
            {

                db.Gen_Mst_Screen.Add(gen_Mst_Screen);
                db.SaveChanges();
                //Session["Email"] = User.Email;
                return RedirectToAction("Index");
            }
            ViewBag.ModuleCode = new SelectList(db.Gen_Mst_Module, "ModuleCode", "ModuleName", gen_Mst_Screen.ModuleCode);
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Screen.ProductCode);
            return View(gen_Mst_Screen);
        }

        // GET: Gen_Mst_Screen/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Screen gen_Mst_Screen = db.Gen_Mst_Screen.FirstOrDefault(m => m.ScreenName == id);
            if (gen_Mst_Screen == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Module, "ProductCode", "ModuleName", gen_Mst_Screen.ProductCode);
            return View(gen_Mst_Screen);
        }

        // POST: Gen_Mst_Screen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,ModuleCode,ScreenCode,ScreenName,Comments,ScreenOrder,CreatedBy,CreatedDate,RowNum")] Gen_Mst_Screen gen_Mst_Screen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_Screen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Module, "ProductCode", "ModuleName", gen_Mst_Screen.ProductCode);
            return View(gen_Mst_Screen);
        }

        // GET: Gen_Mst_Screen/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Screen gen_Mst_Screen = db.Gen_Mst_Screen.FirstOrDefault(m => m.ScreenName == id);
            if (gen_Mst_Screen == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Screen);
        }

        // POST: Gen_Mst_Screen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_Screen gen_Mst_Screen = db.Gen_Mst_Screen.FirstOrDefault(m => m.ScreenName == id);
            db.Gen_Mst_Screen.Remove(gen_Mst_Screen);
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

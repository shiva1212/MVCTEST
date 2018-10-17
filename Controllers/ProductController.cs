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
    
    public class ProductController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Gen_Mst_Product

        public ActionResult Index(String SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortProductNameParameter = string.IsNullOrEmpty(sortBy) ? "ProductName desc" : "";
            ViewBag.sortCommentsParameter = sortBy == "Comments" ? "Comments desc" : "Comments";
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }

            ViewBag.CurrentFilter = SearchString;
            var gen_Mst_Product = db.Gen_Mst_Product.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_Product = gen_Mst_Product.Where(s => s.ProductName.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "ProductName desc":
                    gen_Mst_Product = gen_Mst_Product.OrderByDescending(x => x.ProductName);
                    break;
                case "Comments desc":
                    gen_Mst_Product = gen_Mst_Product.OrderByDescending(x => x.Comments);
                    break;
                case "Comments":
                    gen_Mst_Product = gen_Mst_Product.OrderBy(x => x.Comments);
                    break;
                default:
                    gen_Mst_Product = gen_Mst_Product.OrderBy(x => x.ProductName);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_Product.ToList().ToPagedList(PageNumber, PageSize));
        }

        // GET: Gen_Mst_Product/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Product gen_Mst_Product = db.Gen_Mst_Product.Find(id);
            if (gen_Mst_Product == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Product);
        }

        // GET: Gen_Mst_Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gen_Mst_Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ProductName,Comments")] Gen_Mst_Product gen_Mst_Product)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_Product.Add(gen_Mst_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gen_Mst_Product);
        }

        // GET: Gen_Mst_Product/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Product gen_Mst_Product = db.Gen_Mst_Product.Find(id);
            if (gen_Mst_Product == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Product);
        }

        // POST: Gen_Mst_Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,ProductName,Comments")] Gen_Mst_Product gen_Mst_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gen_Mst_Product);
        }

        // GET: Gen_Mst_Product/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Product gen_Mst_Product = db.Gen_Mst_Product.Find(id);
            if (gen_Mst_Product == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Product);
        }

        // POST: Gen_Mst_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_Product gen_Mst_Product = db.Gen_Mst_Product.Find(id);
            db.Gen_Mst_Product.Remove(gen_Mst_Product);
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

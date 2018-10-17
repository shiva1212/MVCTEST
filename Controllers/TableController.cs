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
   
    public class TableController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Table

        public ActionResult Index(String SearchingString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortTableNameParameter = string.IsNullOrEmpty(sortBy) ? "TableName desc" : "";
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
            var gen_Mst_Table = db.Gen_Mst_Table.AsQueryable();
            if (!String.IsNullOrEmpty(SearchingString))
            {
                gen_Mst_Table = gen_Mst_Table.Include(g => g.Gen_Mst_Product);
                gen_Mst_Table = gen_Mst_Table.Where(s => s.TableName.Contains(SearchingString));
            }
            switch (sortBy)
            {
                case "TableName desc":
                    gen_Mst_Table = gen_Mst_Table.OrderByDescending(x => x.TableName);
                    break;
                case "Date desc":
                    gen_Mst_Table = gen_Mst_Table.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_Table = gen_Mst_Table.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_Table = gen_Mst_Table.OrderBy(x => x.TableName);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_Table.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: Table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Table gen_Mst_Table = db.Gen_Mst_Table.FirstOrDefault(m => m.TableOrder == id);
            if (gen_Mst_Table == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Table);
        }

        // GET: Table/Create
        public ActionResult Create()
        {
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName");
            return View();
        }

        // POST: Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,TableCode,TableName,Comments,TableOrder,CreatedBy,CreatedDate")] Gen_Mst_Table gen_Mst_Table)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_Table.Add(gen_Mst_Table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Table.ProductCode);
            return View(gen_Mst_Table);
        }

        // GET: Table/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Table gen_Mst_Table = db.Gen_Mst_Table.FirstOrDefault(m => m.TableOrder == id);
            if (gen_Mst_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Table.ProductCode);
            return View(gen_Mst_Table);
        }

        // POST: Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,TableCode,TableName,Comments,TableOrder,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Gen_Mst_Table gen_Mst_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_Table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Table.ProductCode);
            return View(gen_Mst_Table);
        }

        // GET: Table/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Table gen_Mst_Table = db.Gen_Mst_Table.FirstOrDefault(m => m.TableOrder == id);
            if (gen_Mst_Table == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Table);
        }

        // POST: Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_Table gen_Mst_Table = db.Gen_Mst_Table.FirstOrDefault(m => m.TableName == id);
            db.Gen_Mst_Table.Remove(gen_Mst_Table);
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

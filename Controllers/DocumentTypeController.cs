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
    
    public class DocumentTypeController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: DocumentType

        public ActionResult Index(int? Page, string SearchString, string currentFilter, string sortBy)
        {
            ViewBag.currentSort = sortBy;
            ViewBag.SortDocCodeParameter = string.IsNullOrEmpty(sortBy) ? "DocCode desc" : "";
            ViewBag.SortDocDescriptionParameter = sortBy == "DocDescription" ? "DocDescription desc" : "DocDescription";
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.currentFilter = SearchString;
            var gen_Mst_DocumentType = db.Gen_Mst_DocumentType.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_DocumentType = gen_Mst_DocumentType.Include(g => g.Rights_MST_UserMaster).Include(g => g.Rights_MST_UserMaster1);
                gen_Mst_DocumentType = gen_Mst_DocumentType.Where(s => s.DocCode.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "DocCode desc":
                    gen_Mst_DocumentType = gen_Mst_DocumentType.OrderByDescending(x => x.DocCode);
                    break;
                case "DocDescription desc":
                    gen_Mst_DocumentType = gen_Mst_DocumentType.OrderByDescending(x => x.DocDescription);
                    break;
                case "DocDescription":
                    gen_Mst_DocumentType = gen_Mst_DocumentType.OrderBy(x => x.DocDescription);
                    break;
                default:
                    gen_Mst_DocumentType = gen_Mst_DocumentType.OrderBy(x => x.DocCode);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_DocumentType.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: DocumentType/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_DocumentType gen_Mst_DocumentType = db.Gen_Mst_DocumentType.FirstOrDefault(m => m.DocCode == id);
            if (gen_Mst_DocumentType == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_DocumentType);
        }

        // GET: DocumentType/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            return View();
        }

        // POST: DocumentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocCode,DocDescription,Comments,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Gen_Mst_DocumentType gen_Mst_DocumentType)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_DocumentType.Add(gen_Mst_DocumentType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DocumentType.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DocumentType.ModifiedBy);
            return View(gen_Mst_DocumentType);
        }

        // GET: DocumentType/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_DocumentType gen_Mst_DocumentType = db.Gen_Mst_DocumentType.FirstOrDefault(m => m.DocCode == id);
            if (gen_Mst_DocumentType == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DocumentType.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DocumentType.ModifiedBy);
            return View(gen_Mst_DocumentType);
        }

        // POST: DocumentType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocCode,DocDescription,Comments,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Gen_Mst_DocumentType gen_Mst_DocumentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_DocumentType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DocumentType.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DocumentType.ModifiedBy);
            return View(gen_Mst_DocumentType);
        }

        // GET: DocumentType/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_DocumentType gen_Mst_DocumentType = db.Gen_Mst_DocumentType.FirstOrDefault(m => m.DocCode == id);
            if (gen_Mst_DocumentType == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_DocumentType);
        }

        // POST: DocumentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_DocumentType gen_Mst_DocumentType = db.Gen_Mst_DocumentType.FirstOrDefault(m => m.DocCode == id);
            db.Gen_Mst_DocumentType.Remove(gen_Mst_DocumentType);
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

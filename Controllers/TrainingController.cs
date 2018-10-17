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
    
    public class TrainingController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: DevTraining

        public ActionResult Index(string SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortTopicNameParameter = string.IsNullOrEmpty(sortBy) ? "TopicName desc" : "";
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
            var gen_Mst_DevTraining = db.Gen_Mst_DevTraining.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_DevTraining = gen_Mst_DevTraining.Include(g => g.Gen_Mst_DocumentType).Include(g => g.Gen_Mst_Technology).Include(g => g.Rights_MST_UserMaster).Include(g => g.Rights_MST_UserMaster1);
                gen_Mst_DevTraining = gen_Mst_DevTraining.Where(s => s.TopicName.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "TopicName desc":
                    gen_Mst_DevTraining = gen_Mst_DevTraining.OrderByDescending(x => x.TopicName);
                    break;
                case "Date desc":
                    gen_Mst_DevTraining = gen_Mst_DevTraining.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_DevTraining = gen_Mst_DevTraining.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_DevTraining = gen_Mst_DevTraining.OrderBy(x => x.TopicName);
                    break;

            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_DevTraining.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: DevTraining/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_DevTraining gen_Mst_DevTraining = db.Gen_Mst_DevTraining.FirstOrDefault(m => m.TopicId == id);
            if (gen_Mst_DevTraining == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_DevTraining);
        }

        // GET: DevTraining/Create
        public ActionResult Create()
        {
            ViewBag.DocCode = new SelectList(db.Gen_Mst_DocumentType, "DocCode", "DocDescription");
            ViewBag.TechCode = new SelectList(db.Gen_Mst_Technology, "TechCode", "TechDescription");
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            return View();
        }

        // POST: DevTraining/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicId,DocCode,TechCode,TopicName,TDescription,TopicOrder,File1,File2,File3,CreatedBy,CreatedDate")] Gen_Mst_DevTraining gen_Mst_DevTraining)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_DevTraining.Add(gen_Mst_DevTraining);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocCode = new SelectList(db.Gen_Mst_DocumentType, "DocCode", "DocDescription", gen_Mst_DevTraining.DocCode);
            ViewBag.TechCode = new SelectList(db.Gen_Mst_Technology, "TechCode", "TechDescription", gen_Mst_DevTraining.TechCode);
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DevTraining.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DevTraining.ModifiedBy);
            return View(gen_Mst_DevTraining);
        }

        // GET: DevTraining/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_DevTraining gen_Mst_DevTraining = db.Gen_Mst_DevTraining.FirstOrDefault(m => m.TopicId == id);
            if (gen_Mst_DevTraining == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocCode = new SelectList(db.Gen_Mst_DocumentType, "DocCode", "DocDescription", gen_Mst_DevTraining.DocCode);
            ViewBag.TechCode = new SelectList(db.Gen_Mst_Technology, "TechCode", "TechDescription", gen_Mst_DevTraining.TechCode);
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DevTraining.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DevTraining.ModifiedBy);
            return View(gen_Mst_DevTraining);
        }

        // POST: DevTraining/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicId,DocCode,TechCode,TopicName,TDescription,TopicOrder,File1,File2,File3,CreatedBy,CreatedDate")] Gen_Mst_DevTraining gen_Mst_DevTraining)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_DevTraining).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocCode = new SelectList(db.Gen_Mst_DocumentType, "DocCode", "DocDescription", gen_Mst_DevTraining.DocCode);
            ViewBag.TechCode = new SelectList(db.Gen_Mst_Technology, "TechCode", "TechDescription", gen_Mst_DevTraining.TechCode);
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DevTraining.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_DevTraining.ModifiedBy);
            return View(gen_Mst_DevTraining);
        }

        // GET: DevTraining/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_DevTraining gen_Mst_DevTraining = db.Gen_Mst_DevTraining.FirstOrDefault(m => m.TopicId == id);
            if (gen_Mst_DevTraining == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_DevTraining);
        }

        // POST: DevTraining/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Gen_Mst_DevTraining gen_Mst_DevTraining = db.Gen_Mst_DevTraining.FirstOrDefault(m => m.TopicId == id);
            db.Gen_Mst_DevTraining.Remove(gen_Mst_DevTraining);
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

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
    
    public class HowDoIController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: HowDoI

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
            var gen_Mst_HowDoI = db.Gen_Mst_HowDoI.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_HowDoI = gen_Mst_HowDoI.Include(g => g.Gen_Mst_Screen);
                gen_Mst_HowDoI = gen_Mst_HowDoI.Where(s => s.TopicName.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "TopicName desc":
                    gen_Mst_HowDoI = gen_Mst_HowDoI.OrderByDescending(x => x.TopicName);
                    break;
                case "Date desc":
                    gen_Mst_HowDoI = gen_Mst_HowDoI.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_HowDoI = gen_Mst_HowDoI.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_HowDoI = gen_Mst_HowDoI.OrderBy(x => x.TopicName);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_HowDoI.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: HowDoI/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_HowDoI gen_Mst_HowDoI = db.Gen_Mst_HowDoI.FirstOrDefault(m => m.TopicName == id);
            if (gen_Mst_HowDoI == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_HowDoI);
        }

        // GET: HowDoI/Create
        public ActionResult Create()
        {
            ViewBag.ScreenCode = new SelectList(db.Gen_Mst_Screen, "ScreenCode", "ScreenName");
            ViewBag.ModuleCode = new SelectList(db.Gen_Mst_Screen, "ModuleCode", "ScreenName");
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Screen, "ProductCode", "ScreenName");
            return View();
        }

        // POST: HowDoI/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ModuleCode,ScreenCode,TopicId,TopicName,TDescription,TopicOrder,DemoSourceFile,WordFile,VideoFile,CreatedBy,CreatedDate,GifFile")] Gen_Mst_HowDoI gen_Mst_HowDoI)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_HowDoI.Add(gen_Mst_HowDoI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScreenCode = new SelectList(db.Gen_Mst_Screen, "ScreenCode", "ScreenName", gen_Mst_HowDoI.ScreenCode);
            ViewBag.ModuoleCode = new SelectList(db.Gen_Mst_Screen, "ModuleCode", "ScreenName", gen_Mst_HowDoI.ModuleCode);
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Screen, "ProductCode", "ScreenName", gen_Mst_HowDoI.ProductCode);
            return View(gen_Mst_HowDoI);
        }

        // GET: HowDoI/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_HowDoI gen_Mst_HowDoI = db.Gen_Mst_HowDoI.FirstOrDefault(m => m.TopicName == id);
            if (gen_Mst_HowDoI == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Screen, "ProductCode", "ScreenName", gen_Mst_HowDoI.ProductCode);
            return View(gen_Mst_HowDoI);
        }

        // POST: HowDoI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,ModuleCode,ScreenCode,TopicId,TopicName,TDescription,TopicOrder,DemoSourceFile,WordFile,VideoFile,CreatedBy,CreatedDate,GifFile")] Gen_Mst_HowDoI gen_Mst_HowDoI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_HowDoI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Screen, "ProductCode", "ScreenName", gen_Mst_HowDoI.ProductCode);
            return View(gen_Mst_HowDoI);
        }

        // GET: HowDoI/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_HowDoI gen_Mst_HowDoI = db.Gen_Mst_HowDoI.FirstOrDefault(m => m.TopicName == id);
            if (gen_Mst_HowDoI == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_HowDoI);
        }

        // POST: HowDoI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_HowDoI gen_Mst_HowDoI = db.Gen_Mst_HowDoI.FirstOrDefault(m => m.TopicName == id);
            db.Gen_Mst_HowDoI.Remove(gen_Mst_HowDoI);
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

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
    
    public class versionController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: version

        public ActionResult Index(String SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortVersionNameParameter = string.IsNullOrEmpty(sortBy) ? "VersionName desc" : "";
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
            var gen_Mst_Version = db.Gen_Mst_version.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_Version = gen_Mst_Version.Where(s => s.versionname.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "VersionName desc":
                    gen_Mst_Version = gen_Mst_Version.OrderByDescending(x => x.versionname);
                    break;
                case "Date desc":
                    gen_Mst_Version = gen_Mst_Version.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Date":
                    gen_Mst_Version = gen_Mst_Version.OrderBy(x => x.CreatedDate);
                    break;
                default:
                    gen_Mst_Version = gen_Mst_Version.OrderBy(x => x.versionname);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_Version.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: version/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_version gen_Mst_version = db.Gen_Mst_version.FirstOrDefault(m => m.CreatedBy == id);
            if (gen_Mst_version == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_version);
        }

        // GET: version/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: version/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "versioncode,versionname,CreatedBy,CreatedDate")] Gen_Mst_version gen_Mst_version)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_version.Add(gen_Mst_version);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gen_Mst_version);
        }

        // GET: version/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_version gen_Mst_version = db.Gen_Mst_version.FirstOrDefault(m => m.CreatedBy == id);
            if (gen_Mst_version == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_version);
        }

        // POST: version/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "versioncode,versionname,CreatedBy,CreatedDate")] Gen_Mst_version gen_Mst_version)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_version).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gen_Mst_version);
        }

        // GET: version/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_version gen_Mst_version = db.Gen_Mst_version.FirstOrDefault(m => m.CreatedBy == id);
            if (gen_Mst_version == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_version);
        }

        // POST: version/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_version gen_Mst_version = db.Gen_Mst_version.FirstOrDefault(m => m.CreatedBy == id);
            db.Gen_Mst_version.Remove(gen_Mst_version);
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

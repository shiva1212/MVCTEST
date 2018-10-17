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
    
    public class TeamsController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Teams

        public ActionResult Index(String SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortTeamCodeParameter = string.IsNullOrEmpty(sortBy) ? "TeamCode desc" : "";
            ViewBag.SortTeamDescriptionParameter = sortBy == "TeamDescription" ? "TeamDescription desc" : "TeamDescription";
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var gen_Mst_Teams = db.Gen_Mst_Teams.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_Teams = gen_Mst_Teams.Include(g => g.Rights_MST_UserMaster).Include(g => g.Rights_MST_UserMaster1);
                gen_Mst_Teams = gen_Mst_Teams.Where(s => s.TeamCode.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "TeamCode desc":
                    gen_Mst_Teams = gen_Mst_Teams.OrderByDescending(x => x.TeamCode);
                    break;
                case "TeamDescription desc":
                    gen_Mst_Teams = gen_Mst_Teams.OrderByDescending(x => x.TeamDescription);
                    break;
                case "TeamDescription":
                    gen_Mst_Teams = gen_Mst_Teams.OrderBy(x => x.TeamDescription);
                    break;
                default:
                    gen_Mst_Teams = gen_Mst_Teams.OrderBy(x => x.TeamCode);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_Teams.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: Teams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Teams gen_Mst_Teams = db.Gen_Mst_Teams.FirstOrDefault(m => m.TeamCode == id);
            if (gen_Mst_Teams == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Teams);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamCode,TeamDescription,Comments,CreatedBy,CreatedDate")] Gen_Mst_Teams gen_Mst_Teams)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_Teams.Add(gen_Mst_Teams);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Teams.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Teams.ModifiedBy);
            return View(gen_Mst_Teams);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Teams gen_Mst_Teams = db.Gen_Mst_Teams.FirstOrDefault(m => m.TeamCode == id);
            if (gen_Mst_Teams == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Teams.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Teams.ModifiedBy);
            return View(gen_Mst_Teams);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamCode,TeamDescription,Comments,CreatedBy,CreatedDate,")] Gen_Mst_Teams gen_Mst_Teams)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_Teams).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Teams.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Teams.ModifiedBy);
            return View(gen_Mst_Teams);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Teams gen_Mst_Teams = db.Gen_Mst_Teams.FirstOrDefault(m => m.TeamCode == id);
            if (gen_Mst_Teams == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Teams);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_Teams gen_Mst_Teams = db.Gen_Mst_Teams.FirstOrDefault(m => m.TeamCode == id);
            db.Gen_Mst_Teams.Remove(gen_Mst_Teams);
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

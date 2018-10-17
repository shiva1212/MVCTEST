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
    
    public class ClientController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: Gen_Mst_Client
        public ActionResult Index(String SearchString, string CurrentFilter, int? Page, string sortBy)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortAddress1Parameter = sortBy == "Address1" ? "Address1 desc" : "Address1";
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var gen_Mst_Client = db.Gen_Mst_Client.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                gen_Mst_Client = gen_Mst_Client.Include(g => g.Gen_Mst_Product).Include(g => g.Rights_MST_UserMaster).Include(g => g.Rights_MST_UserMaster1);
                gen_Mst_Client = gen_Mst_Client.Where(x => x.Name.Contains(SearchString));
            }
            switch (sortBy)
            {
                case "Name desc":
                    gen_Mst_Client = gen_Mst_Client.OrderByDescending(x => x.Name);
                    break;
                case "Address1 desc":
                    gen_Mst_Client = gen_Mst_Client.OrderByDescending(x => x.Address1);
                    break;
                case "Address1":
                    gen_Mst_Client = gen_Mst_Client.OrderBy(x => x.Address1);
                    break;
                default:
                    gen_Mst_Client = gen_Mst_Client.OrderBy(x => x.Name);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(gen_Mst_Client.ToList().ToPagedList(PageNumber, PageSize));


        }

        // GET: Gen_Mst_Client/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Client gen_Mst_Client = db.Gen_Mst_Client.FirstOrDefault(m => m.ClientCode == id);
            if (gen_Mst_Client == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Client);
        }

        // GET: Gen_Mst_Client/Create
        public ActionResult Create()
        {
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName");
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name");
            return View();
        }

        // POST: Gen_Mst_Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ClientCode,Name,Address1,Address2,CityName,StateName,ZipCode,Email,Phone,Fax,CreatedDate,CreatedBy,SendConfirm,MedazVersion,Sqlversion")] Gen_Mst_Client gen_Mst_Client)
        {
            if (ModelState.IsValid)
            {
                db.Gen_Mst_Client.Add(gen_Mst_Client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Client.ProductCode);
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Client.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Client.ModifiedBy);
            return View(gen_Mst_Client);
        }

        // GET: Gen_Mst_Client/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Client gen_Mst_Client = db.Gen_Mst_Client.FirstOrDefault(m => m.ClientCode == id);
            if (gen_Mst_Client == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Client.ProductCode);
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Client.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Client.ModifiedBy);
            return View(gen_Mst_Client);
        }

        // POST: Gen_Mst_Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,ClientCode,Name,Address1,Address2,CityName,StateName,ZipCode,Email,Phone,Fax,CreatedDate,CreatedBy,SendConfirm,MedazVersion,Sqlversion")] Gen_Mst_Client gen_Mst_Client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Mst_Client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCode = new SelectList(db.Gen_Mst_Product, "ProductCode", "ProductName", gen_Mst_Client.ProductCode);
            ViewBag.CreatedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Client.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Rights_MST_UserMaster, "UserId", "Name", gen_Mst_Client.ModifiedBy);
            return View(gen_Mst_Client);
        }

        // GET: Gen_Mst_Client/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Mst_Client gen_Mst_Client = db.Gen_Mst_Client.FirstOrDefault(m => m.ClientCode == id);
            if (gen_Mst_Client == null)
            {
                return HttpNotFound();
            }
            return View(gen_Mst_Client);
        }

        // POST: Gen_Mst_Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Gen_Mst_Client gen_Mst_Client = db.Gen_Mst_Client.FirstOrDefault(m => m.ClientCode == id);
            db.Gen_Mst_Client.Remove(gen_Mst_Client);
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

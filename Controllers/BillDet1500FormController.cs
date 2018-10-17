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

    public class BillDet1500FormController : Controller
    {
        private intissuesEntities2 db = new intissuesEntities2();

        // GET: BillDet1500Form
        public ActionResult Index(int? Page, String SearchString, string CurrentFilter, string SortBy)
        {
            ViewBag.CurrentSort = SortBy;
            ViewBag.SortClientCodeParameter = string.IsNullOrEmpty(SortBy) ? "ClientCode desc" : "";
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var bill_Det_1500Form = db.Bill_Det_1500Form.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                bill_Det_1500Form = bill_Det_1500Form.Include(b => b.Bill_Mst_1500Form);
                bill_Det_1500Form = bill_Det_1500Form.Where(s => s.ClientCode.Contains(SearchString));
            }
            switch (SortBy)
            {
                case "ClientCode desc":
                    bill_Det_1500Form = bill_Det_1500Form.OrderByDescending(x => x.ClientCode);
                    break;
                default:
                    bill_Det_1500Form = bill_Det_1500Form.OrderBy(x => x.ClientCode);
                    break;
            }
            int PageSize = 10;
            int PageNumber = (Page ?? 1);
            return View(bill_Det_1500Form.ToList().ToPagedList(PageNumber, PageSize));

        }

        // GET: BillDet1500Form/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_Det_1500Form bill_Det_1500Form = db.Bill_Det_1500Form.FirstOrDefault(m => m.RowNum == id);
            if (bill_Det_1500Form == null)
            {
                return HttpNotFound();
            }
            return View(bill_Det_1500Form);
        }

        // GET: BillDet1500Form/Create
        public ActionResult Create()
        {
            ViewBag.ReferenceNo = new SelectList(db.Bill_Mst_1500Form, "ReferenceNo", "ClientCode");
            return View();
        }

        // POST: BillDet1500Form/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RowNum,ReferenceNo,ClientCode,BillNo,BoxNo1Medicare,BoxNo1Medicaid,BoxNo1Tricare,BoxNo1CHAMPVA,BoxNo1Group,BoxNo1FECA,BoxNo1Other,BoxNo1a,BoxNo2,BoxNo3DOBMM,BoxNo3DOBDD,BoxNo3DOBYY,BoxNo3SexMale,BoxNo3SexFemale,BoxNo4,BoxNo5Add,BoxNo5City,BoxNo5Sta,BoxNo5Zip,BoxNo5Area,BoxNo5Phone,BoxNo6Self,BoxNo6Spouse,BoxNo6Child,BoxNo6Other,BoxNo7Add,BoxNo7City,BoxNo7Sta,BoxNo7Zip,BoxNo7Area,BoxNo7Phone,BoxNo8Single,BoxNo8Married,BoxNo8Other,BoxNo8Employed,BoxNo8FullTimeStudent,BoxNo8PartTimeStudent,BoxNo9,BoxNo9a,BoxNo9bDOBMM,BoxNo9bDOBDD,BoxNo9bDOBYY,BoxNo9bSexMale,BoxNo9bSexFemale,BoxNo9c,BoxNo9d,BoxNo10aYes,BoxNo10aNo,BoxNo10bYes,BoxNo10bNo,BoxNo10bPlace,BoxNo10cYes,BoxNo10cNo,BoxNo10d,BoxNo11,BoxNo11aDOBMM,BoxNo11aDOBDD,BoxNo11aDOBYY,BoxNo11aSexMale,BoxNo11aSexFemale,BoxNo11b,BoxNo11c,BoxNo11dYes,BoxNo11dNo,BoxNo12Sign,BoxNo12Date,BoxNo13Sign,BoxNo14MM,BoxNo14DD,BoxNo14YY,BoxNo15MM,BoxNo15DD,BoxNo15YY,BoxNo16FromMM,BoxNo16FromDD,BoxNo16FromYY,BoxNo16ToMM,BoxNo16ToDD,BoxNo16ToYY,BoxNo17,BoxNo17A,BoxNo17NPI,BoxNo18FromMM,BoxNo18FromDD,BoxNo18FromYY,BoxNo18ToMM,BoxNo18ToDD,BoxNo18ToYY,BoxNo19,BoxNo20aYes,BoxNo20aNo,BoxNo20bChargesA,BoxNo20bChargesB,BoxNo21IcdCode1,BoxNo21IcdCode2,BoxNo21IcdCode3,BoxNo21IcdCode4,BoxNo21IcdCode5,BoxNo21IcdCode6,BoxNo22Code,BoxNo22RefNo,BoxNo23,BoxNo24AFromMM,BoxNo24AFromDD,BoxNo24AFromYY,BoxNo24AToMM,BoxNo24AToDD,BoxNo24AToYY,BoxNo24B,BoxNo24C,BoxNo24Code,BoxNo24Mod1,BoxNo24Mod2,BoxNo24Mod3,BoxNo24Mod4,BoxNo24E,BilledAmt,BoxNo24VisitDtOhio,BilledDateOhio,BoxNo24FOh,BoxNo24F,BoxNo24FDec,BoxNo24G,BoxNo24H,BoxNo24I,BoxNo24Ja,BoxNo24JNPI,BoxNo25a,BoxNo25bSSN,BoxNo25bEIN,BoxNo26,BoxNo27Yes,BoxNo27No,BoxNo31a,BoxNo31b,BoxNo32a,BoxNo32b,BoxNo32c,BoxNo32aNPI,BoxNo33a,BoxNo33b,BoxNo33c,BoxNo33d,BoxNo33e,BoxNo33F,SortOrder,InsuranceFlag,PrintLine,Sno,InsuranceCode,InsuranceName,InsAddress1,InsHead2,InsHead3,InsCityName,InsStateCode,InsZipCode,FromVisitDate,PatLastName,PatFirstName,ProviderAddress,ProCity,ProState,ProZip,MedicalRecordNo,ProviderServiceCode,BilledDate,GroupID,AllowedAmt,Deductable,CoIns,DatePaidByMedicar,Insamount,InsPayment,InsHCFABox29,InsHCFABox30")] Bill_Det_1500Form bill_Det_1500Form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Bill_Det_1500Form.Add(bill_Det_1500Form);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {

                ViewBag.ReferenceNo = new SelectList(db.Bill_Mst_1500Form, "ReferenceNo", "ClientCode", bill_Det_1500Form.ReferenceNo);
                return View(bill_Det_1500Form);
            }
        }

        // GET: BillDet1500Form/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_Det_1500Form bill_Det_1500Form = db.Bill_Det_1500Form.FirstOrDefault(m => m.RowNum == id);
            if (bill_Det_1500Form == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReferenceNo = new SelectList(db.Bill_Mst_1500Form, "ReferenceNo", "ClientCode", bill_Det_1500Form.ReferenceNo);
            return View(bill_Det_1500Form);
        }

        // POST: BillDet1500Form/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RowNum,ReferenceNo,ClientCode,BillNo,BoxNo1Medicare,BoxNo1Medicaid,BoxNo1Tricare,BoxNo1CHAMPVA,BoxNo1Group,BoxNo1FECA,BoxNo1Other,BoxNo1a,BoxNo2,BoxNo3DOBMM,BoxNo3DOBDD,BoxNo3DOBYY,BoxNo3SexMale,BoxNo3SexFemale,BoxNo4,BoxNo5Add,BoxNo5City,BoxNo5Sta,BoxNo5Zip,BoxNo5Area,BoxNo5Phone,BoxNo6Self,BoxNo6Spouse,BoxNo6Child,BoxNo6Other,BoxNo7Add,BoxNo7City,BoxNo7Sta,BoxNo7Zip,BoxNo7Area,BoxNo7Phone,BoxNo8Single,BoxNo8Married,BoxNo8Other,BoxNo8Employed,BoxNo8FullTimeStudent,BoxNo8PartTimeStudent,BoxNo9,BoxNo9a,BoxNo9bDOBMM,BoxNo9bDOBDD,BoxNo9bDOBYY,BoxNo9bSexMale,BoxNo9bSexFemale,BoxNo9c,BoxNo9d,BoxNo10aYes,BoxNo10aNo,BoxNo10bYes,BoxNo10bNo,BoxNo10bPlace,BoxNo10cYes,BoxNo10cNo,BoxNo10d,BoxNo11,BoxNo11aDOBMM,BoxNo11aDOBDD,BoxNo11aDOBYY,BoxNo11aSexMale,BoxNo11aSexFemale,BoxNo11b,BoxNo11c,BoxNo11dYes,BoxNo11dNo,BoxNo12Sign,BoxNo12Date,BoxNo13Sign,BoxNo14MM,BoxNo14DD,BoxNo14YY,BoxNo15MM,BoxNo15DD,BoxNo15YY,BoxNo16FromMM,BoxNo16FromDD,BoxNo16FromYY,BoxNo16ToMM,BoxNo16ToDD,BoxNo16ToYY,BoxNo17,BoxNo17A,BoxNo17NPI,BoxNo18FromMM,BoxNo18FromDD,BoxNo18FromYY,BoxNo18ToMM,BoxNo18ToDD,BoxNo18ToYY,BoxNo19,BoxNo20aYes,BoxNo20aNo,BoxNo20bChargesA,BoxNo20bChargesB,BoxNo21IcdCode1,BoxNo21IcdCode2,BoxNo21IcdCode3,BoxNo21IcdCode4,BoxNo21IcdCode5,BoxNo21IcdCode6,BoxNo22Code,BoxNo22RefNo,BoxNo23,BoxNo24AFromMM,BoxNo24AFromDD,BoxNo24AFromYY,BoxNo24AToMM,BoxNo24AToDD,BoxNo24AToYY,BoxNo24B,BoxNo24C,BoxNo24Code,BoxNo24Mod1,BoxNo24Mod2,BoxNo24Mod3,BoxNo24Mod4,BoxNo24E,BilledAmt,BoxNo24VisitDtOhio,BilledDateOhio,BoxNo24FOh,BoxNo24F,BoxNo24FDec,BoxNo24G,BoxNo24H,BoxNo24I,BoxNo24Ja,BoxNo24JNPI,BoxNo25a,BoxNo25bSSN,BoxNo25bEIN,BoxNo26,BoxNo27Yes,BoxNo27No,BoxNo31a,BoxNo31b,BoxNo32a,BoxNo32b,BoxNo32c,BoxNo32aNPI,BoxNo33a,BoxNo33b,BoxNo33c,BoxNo33d,BoxNo33e,BoxNo33F,SortOrder,InsuranceFlag,PrintLine,Sno,InsuranceCode,InsuranceName,InsAddress1,InsHead2,InsHead3,InsCityName,InsStateCode,InsZipCode,FromVisitDate,PatLastName,PatFirstName,ProviderAddress,ProCity,ProState,ProZip,MedicalRecordNo,ProviderServiceCode,BilledDate,GroupID,AllowedAmt,Deductable,CoIns,DatePaidByMedicar,Insamount,InsPayment,InsHCFABox29,InsHCFABox30")] Bill_Det_1500Form bill_Det_1500Form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill_Det_1500Form).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReferenceNo = new SelectList(db.Bill_Mst_1500Form, "ReferenceNo", "ClientCode", bill_Det_1500Form.ReferenceNo);
            return View(bill_Det_1500Form);
        }

        // GET: BillDet1500Form/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_Det_1500Form bill_Det_1500Form = db.Bill_Det_1500Form.FirstOrDefault(m => m.RowNum == id);
            if (bill_Det_1500Form == null)
            {
                return HttpNotFound();
            }
            return View(bill_Det_1500Form);
        }

        // POST: BillDet1500Form/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Bill_Det_1500Form bill_Det_1500Form = db.Bill_Det_1500Form.FirstOrDefault(m => m.RowNum == id);
            db.Bill_Det_1500Form.Remove(bill_Det_1500Form);
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

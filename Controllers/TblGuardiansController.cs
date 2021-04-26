using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OneToOne;

namespace OneToOne.Controllers
{
    public class TblGuardiansController : Controller
    {
        private OnetoOneDBEntities db = new OnetoOneDBEntities();

        // GET: TblGuardians
        public ActionResult Index()
        {
            var tblGuardians = db.TblGuardians.Include(t => t.TblStudent);
            return View(tblGuardians.ToList());
        }

        // GET: TblGuardians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblGuardian tblGuardian = db.TblGuardians.Find(id);
            if (tblGuardian == null)
            {
                return HttpNotFound();
            }
            return View(tblGuardian);
        }

        // GET: TblGuardians/Create
        public ActionResult Create()
        {
            ViewBag.R_no = new SelectList(db.TblStudents, "R_no", "Std_Name");
            return View();
        }

        // POST: TblGuardians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "R_no,G_no,G_Name")] TblGuardian tblGuardian)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.TblGuardians.Add(tblGuardian);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.m = "Invalid Data User";
                }
            }

            ViewBag.R_no = new SelectList(db.TblStudents, "R_no", "Std_Name", tblGuardian.R_no);
            return View(tblGuardian);
        }

        // GET: TblGuardians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblGuardian tblGuardian = db.TblGuardians.Find(id);
            if (tblGuardian == null)
            {
                return HttpNotFound();
            }
            ViewBag.R_no = new SelectList(db.TblStudents, "R_no", "Std_Name", tblGuardian.R_no);
            return View(tblGuardian);
        }

        // POST: TblGuardians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "R_no,G_no,G_Name")] TblGuardian tblGuardian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGuardian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.R_no = new SelectList(db.TblStudents, "R_no", "Std_Name", tblGuardian.R_no);
            return View(tblGuardian);
        }

        // GET: TblGuardians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblGuardian tblGuardian = db.TblGuardians.Find(id);
            if (tblGuardian == null)
            {
                return HttpNotFound();
            }
            return View(tblGuardian);
        }

        // POST: TblGuardians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblGuardian tblGuardian = db.TblGuardians.Find(id);
            db.TblGuardians.Remove(tblGuardian);
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

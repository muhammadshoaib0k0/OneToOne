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
    public class TblStudentsController : Controller
    {
        private OnetoOneDBEntities db = new OnetoOneDBEntities();

        // GET: TblStudents
        public ActionResult Index()
        {
            var tblStudents = db.TblStudents.Include(t => t.TblGuardian);
            return View(tblStudents.ToList());
        }

        // GET: TblStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblStudent tblStudent = db.TblStudents.Find(id);
            if (tblStudent == null)
            {
                return HttpNotFound();
            }
            return View(tblStudent);
        }

        // GET: TblStudents/Create
        public ActionResult Create()
        {
            ViewBag.R_no = new SelectList(db.TblGuardians, "R_no", "G_Name");
            return View();
        }

        // POST: TblStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "R_no,Std_Name,Std_Phn,Std_Email,CNIC,SUBJECT")] TblStudent tblStudent)
        {
            if (ModelState.IsValid)
            {
                db.TblStudents.Add(tblStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.R_no = new SelectList(db.TblGuardians, "R_no", "G_Name", tblStudent.R_no);
            return View(tblStudent);
        }

        // GET: TblStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblStudent tblStudent = db.TblStudents.Find(id);
            if (tblStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.R_no = new SelectList(db.TblGuardians, "R_no", "G_Name", tblStudent.R_no);
            return View(tblStudent);
        }

        // POST: TblStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "R_no,Std_Name,Std_Phn,Std_Email,CNIC,SUBJECT")] TblStudent tblStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.R_no = new SelectList(db.TblGuardians, "R_no", "G_Name", tblStudent.R_no);
            return View(tblStudent);
        }

        // GET: TblStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblStudent tblStudent = db.TblStudents.Find(id);
            if (tblStudent == null)
            {
                return HttpNotFound();
            }
            return View(tblStudent);
        }

        // POST: TblStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblStudent tblStudent = db.TblStudents.Find(id);
            db.TblStudents.Remove(tblStudent);
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

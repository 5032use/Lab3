using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_MyModelFirst.Models;

namespace FIT5032_MyModelFirst.Controllers
{
    public class UnitsController : Controller
    {
        private FIT5032_MyModelFirstContainer db = new FIT5032_MyModelFirstContainer();

        // GET: Units
        public ActionResult Index()
        {
            var unitsSet = db.UnitsSet.Include(u => u.Student);
            return View(unitsSet.ToList());
        }

        // GET: Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units units = db.UnitsSet.Find(id);
            if (units == null)
            {
                return HttpNotFound();
            }
            return View(units);
        }

        // GET: Units/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.StudentSet, "Id", "FirstName");
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StudentId")] Units units)
        {
            if (ModelState.IsValid)
            {
                db.UnitsSet.Add(units);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.StudentSet, "Id", "FirstName", units.StudentId);
            return View(units);
        }

        // GET: Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units units = db.UnitsSet.Find(id);
            if (units == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.StudentSet, "Id", "FirstName", units.StudentId);
            return View(units);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StudentId")] Units units)
        {
            if (ModelState.IsValid)
            {
                db.Entry(units).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.StudentSet, "Id", "FirstName", units.StudentId);
            return View(units);
        }

        // GET: Units/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units units = db.UnitsSet.Find(id);
            if (units == null)
            {
                return HttpNotFound();
            }
            return View(units);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Units units = db.UnitsSet.Find(id);
            db.UnitsSet.Remove(units);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CartPhill.Models;

namespace CartPhill.Controllers
{
    public class OPPsController : Controller
    {
        private Product db = new Product();

        // GET: OPPs
        public ActionResult Index()
        {
            return View(db.Hoards.ToList());
        }

        // GET: OPPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPP oPP = db.Hoards.Find(id);
            if (oPP == null)
            {
                return HttpNotFound();
            }
            return View(oPP);
        }

        // GET: OPPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OPPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Description,Catagory")] OPP oPP)
        {
            if (ModelState.IsValid)
            {
                db.Hoards.Add(oPP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oPP);
        }

        // GET: OPPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPP oPP = db.Hoards.Find(id);
            if (oPP == null)
            {
                return HttpNotFound();
            }
            return View(oPP);
        }

        // POST: OPPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Description,Catagory")] OPP oPP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oPP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oPP);
        }

        // GET: OPPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPP oPP = db.Hoards.Find(id);
            if (oPP == null)
            {
                return HttpNotFound();
            }
            return View(oPP);
        }

        // POST: OPPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OPP oPP = db.Hoards.Find(id);
            db.Hoards.Remove(oPP);
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

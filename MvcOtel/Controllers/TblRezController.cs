using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcOtel.Models;

namespace MvcOtel.Controllers
{
    public class TblRezController : Controller
    {
        private MvcOtelEntities db = new MvcOtelEntities();

        // GET: TblRez
        public ActionResult Index()
        {
            var tblRezs = db.TblRezs.Include(t => t.TblKullanici).Include(t => t.TblOda).Include(t => t.TblOtel);
            return View(tblRezs.ToList());
        }

        // GET: TblRez/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRez tblRez = db.TblRezs.Find(id);
            if (tblRez == null)
            {
                return HttpNotFound();
            }
            return View(tblRez);
        }

        // GET: TblRez/Create
        public ActionResult Create()
        {
            ViewBag.KId = new SelectList(db.TblKullanicis, "KId", "KAd");
            ViewBag.OdaId = new SelectList(db.TblOdas, "OdaId", "OdaTür");
            ViewBag.OtelId = new SelectList(db.TblOtels, "OtelId", "OtelAd");
            return View();
        }

        // POST: TblRez/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RezId,KId,OtelId,OdaId,RezTarih,RezGün")] TblRez tblRez)
        {
            if (ModelState.IsValid)
            {
                db.TblRezs.Add(tblRez);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KId = new SelectList(db.TblKullanicis, "KId", "KAd", tblRez.KId);
            ViewBag.OdaId = new SelectList(db.TblOdas, "OdaId", "OdaTür", tblRez.OdaId);
            ViewBag.OtelId = new SelectList(db.TblOtels, "OtelId", "OtelAd", tblRez.OtelId);
            return View(tblRez);
        }

        // GET: TblRez/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRez tblRez = db.TblRezs.Find(id);
            if (tblRez == null)
            {
                return HttpNotFound();
            }
            ViewBag.KId = new SelectList(db.TblKullanicis, "KId", "KAd", tblRez.KId);
            ViewBag.OdaId = new SelectList(db.TblOdas, "OdaId", "OdaTür", tblRez.OdaId);
            ViewBag.OtelId = new SelectList(db.TblOtels, "OtelId", "OtelAd", tblRez.OtelId);
            return View(tblRez);
        }

        // POST: TblRez/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RezId,KId,OtelId,OdaId,RezTarih,RezGün")] TblRez tblRez)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRez).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KId = new SelectList(db.TblKullanicis, "KId", "KAd", tblRez.KId);
            ViewBag.OdaId = new SelectList(db.TblOdas, "OdaId", "OdaTür", tblRez.OdaId);
            ViewBag.OtelId = new SelectList(db.TblOtels, "OtelId", "OtelAd", tblRez.OtelId);
            return View(tblRez);
        }

        // GET: TblRez/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRez tblRez = db.TblRezs.Find(id);
            return View(tblRez);
        }

        // POST: TblRez/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblRez tblRez = db.TblRezs.Find(id);
            db.TblRezs.Remove(tblRez);
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

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
    public class YöneticiController : Controller
    {
        private MvcOtelEntities db = new MvcOtelEntities();
        public ActionResult YöneticiGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YöneticiGiris(TblYönetici Model)
        {
            var Yönetici = db.TblYönetici.FirstOrDefault(x => x.YTcNo == Model.YTcNo && x.YSifre == Model.YSifre);
            if (Yönetici != null)
            {
                Session["YTcNo"] = Yönetici;
                return RedirectToAction("YöneticiPanel", "Yönetici", Yönetici);
            }
            else
            {
                ViewBag.Hata("Kullanıcı Adı veya Şifre Hatalı.");
                return View();
            }
        }
        public ActionResult YöneticiPanel(TblYönetici Model)
        {
            return View(db.TblRezs.Where(d => d.OtelId == Model.OtelId).ToList());
        }
        // GET: Yönetici
        public ActionResult Index()
        {
            var tblYönetici = db.TblYönetici.Include(t => t.TblOtel);
            return View(tblYönetici.ToList());
        }
        public ActionResult Exit()
        {
            return View();
        }
        public ActionResult OtelIst()
        {
            return View();
        }

        // GET: Yönetici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblYönetici tblYönetici = db.TblYönetici.Find(id);
            if (tblYönetici == null)
            {
                return HttpNotFound();
            }
            return View(tblYönetici);
        }

        // GET: Yönetici/Create
        public ActionResult Create()
        {
            ViewBag.OtelId = new SelectList(db.TblOtels, "OtelId", "OtelAd");
            return View();
        }

        // POST: Yönetici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YId,YAd,YSoyad,YTcNo,YSifre,OtelId")] TblYönetici tblYönetici)
        {
            if (ModelState.IsValid)
            {
                db.TblYönetici.Add(tblYönetici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OtelId = new SelectList(db.TblOtels, "OtelId", "OtelAd", tblYönetici.OtelId);
            return View(tblYönetici);
        }

        // GET: Yönetici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblYönetici tblYönetici = db.TblYönetici.Find(id);
            if (tblYönetici == null)
            {
                return HttpNotFound();
            }
            ViewBag.OtelId = new SelectList(db.TblOtels, "OtelId", "OtelAd", tblYönetici.OtelId);
            return View(tblYönetici);
        }

        // POST: Yönetici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YId,YAd,YSoyad,YTcNo,YSifre,OtelId")] TblYönetici tblYönetici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblYönetici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OtelId = new SelectList(db.TblOtels, "OtelId", "OtelAd", tblYönetici.OtelId);
            return View(tblYönetici);
        }

        // GET: Yönetici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblYönetici tblYönetici = db.TblYönetici.Find(id);
            if (tblYönetici == null)
            {
                return HttpNotFound();
            }
            return View(tblYönetici);
        }

        // POST: Yönetici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblYönetici tblYönetici = db.TblYönetici.Find(id);
            db.TblYönetici.Remove(tblYönetici);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MvcOtel.Models;

namespace MvcOtel.Controllers
{
    public class KullaniciController : Controller
    {
        private MvcOtelEntities db = new MvcOtelEntities();
        // GET: Kullanici
        public ActionResult KullaniciGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KullaniciGiris(TblKullanici Model)
        {
            var Kullanici = db.TblKullanicis.FirstOrDefault(x => x.KTcNo == Model.KTcNo && x.KSifre == Model.KSifre);
            if (Kullanici != null)
            {
                Session["KTcNo"] = Kullanici;
                ViewBag.KulID = Kullanici.KId;
                return RedirectToAction("KullaniciPanel","Kullanici",Kullanici);
            }
            else {
                ViewBag.Hata("Kullanıcı Adı veya Şifre Hatalı.");
                return View();
            }
        }
        public ActionResult KullaniciPanel(TblKullanici Model)
        {
            return View(db.TblRezs.Where(d => d.KId == Model.KId).ToList());
        }
        public ActionResult KayıtBasarılı()
        {
            return View();
        }
        public ActionResult RezOlustur()
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
        public ActionResult RezOlustur([Bind(Include = "RezId,KId,OtelId,OdaId,RezTarih,RezGün")] TblRez tblRez)
        {
            if (ModelState.IsValid)
            {
                db.TblRezs.Add(tblRez);
                db.SaveChanges();
                var Kullanici = db.TblKullanicis.FirstOrDefault(x => x.KId == tblRez.KId);
                return RedirectToAction("KullaniciPanel","Kullanici",Kullanici);
            }

            ViewBag.KId = new SelectList(db.TblKullanicis, "KId", "KAd", tblRez.KId);
            ViewBag.OdaId = new SelectList(db.TblOdas, "OdaId", "OdaTür", tblRez.OdaId);
            ViewBag.OtelId = new SelectList(db.TblOtels, "OtelId", "OtelAd", tblRez.OtelId);
            return View(tblRez);
        }
        public ActionResult RezSil(int id)
        {
            var SilRez = db.TblRezs.FirstOrDefault(x => x.RezId == id);
            var Kullanici = db.TblKullanicis.FirstOrDefault(x => x.KId == SilRez.KId);
            db.TblRezs.Remove(SilRez);
            db.SaveChanges();
            return RedirectToAction("KullaniciPanel", "Kullanici", Kullanici);
        }
        public ActionResult Exit()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View(db.TblKullanicis.ToList());
        }

        // GET: Kullanici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblKullanici tblKullanici = db.TblKullanicis.Find(id);
            if (tblKullanici == null)
            {
                return HttpNotFound();
            }
            return View(tblKullanici);
        }

        // GET: Kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KId,KAd,KSoyad,KYas,KTcNo,KSifre")] TblKullanici tblKullanici)
        {
            if (ModelState.IsValid)
            {
                db.TblKullanicis.Add(tblKullanici);
                db.SaveChanges();
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                ViewBag.Hata("Bilgilerinizi eksik girdiniz.");
            }
            return View(tblKullanici);
        }

        // GET: Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblKullanici tblKullanici = db.TblKullanicis.Find(id);
            if (tblKullanici == null)
            {
                return HttpNotFound();
            }
            return View(tblKullanici);
        }

        // POST: Kullanici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KId,KAd,KSoyad,KYas,KTcNo,KSifre")] TblKullanici tblKullanici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblKullanici).State = EntityState.Modified;
                db.SaveChanges();
                var Kullanici = db.TblKullanicis.FirstOrDefault(x => x.KId == tblKullanici.KId);
                return RedirectToAction("KullaniciPanel", "Kullanici", Kullanici);
            }
            else
            {
                var Kullanici = db.TblKullanicis.FirstOrDefault(x => x.KId == tblKullanici.KId);
                return RedirectToAction("KullaniciPanel", "Kullanici", Kullanici);
            }
        }

        // GET: Kullanici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblKullanici tblKullanici = db.TblKullanicis.Find(id);
            if (tblKullanici == null)
            {
                return HttpNotFound();
            }
            return View(tblKullanici);
        }

        // POST: Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblKullanici tblKullanici = db.TblKullanicis.Find(id);
            db.TblKullanicis.Remove(tblKullanici);
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

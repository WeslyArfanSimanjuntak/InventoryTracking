using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Application.DataModel;
using EntityState = System.Data.Entity.EntityState;

namespace Web.MainApplication.Controllers
{
    public class PlaceController : BaseController
    {
        private DBEntities db = new DBEntities();

        // GET: Place
        public ActionResult Index()
        {
            var place = db.place.Include(p => p.place2);
            return View(place.ToList());
        }

        // GET: Place/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            place place = db.place.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: Place/Create
        public ActionResult Create()
        {
            ViewBag.idParent = new SelectList(db.place, "id", "code");
            return View();
        }

        // POST: Place/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idParent,code,nama,location,floorLevel,remark,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsActive")] place place)
        {
            if (ModelState.IsValid)
            {
                place.SetPropertyCreate();
                db.place.Add(place);
                db.SaveChanges(); 
                SuccessMessagesAdd(Message.CreateSuccess);
                return RedirectToAction("Index");
            }

            ViewBag.idParent = new SelectList(db.place, "id", "code", place.idParent);
            return View(place);
        }

        // GET: Place/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            place place = db.place.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            ViewBag.idParent = new SelectList(db.place, "id", "code", place.idParent);
            return View(place);
        }

        // POST: Place/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idParent,code,nama,location,floorLevel,remark,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsActive")] place place)
        {
            if (ModelState.IsValid)
            {
                place.SetPropertyUpdate();
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                SuccessMessagesAdd(Message.UpdateSuccess);
                return RedirectToAction("Index");

            }
            ViewBag.idParent = new SelectList(db.place, "id", "code", place.idParent);
            return View(place);
        }

        // GET: Place/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            place place = db.place.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Place/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            place place = db.place.Find(id);
            db.place.Remove(place);
            db.SaveChanges();
            SuccessMessagesAdd(Message.DeleteSuccess);
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

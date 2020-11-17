using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Application.DataModel;
using RestSharp;
using EntityState = System.Data.Entity.EntityState;

namespace Web.MainApplication.Controllers
{
    public class AssetController : BaseController
    {
        private DBEntities db = new DBEntities();

        // GET: Asset
        public ActionResult Index()
        {

            var client = new RestClient("http://localhost/iHuman/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", "Inventory");
            request.AddParameter("password", "Inventory");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            //var client = new RestClient("http://localhost/iHuman/api/Employee/EmployeesBirthday?totalRecord=4");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Bearer tjLt9Q9woRdKoprJPNIy0dFzeWEgAEaFnrEFFf_kdWYzRk0moxYdz-HfVo1M1HKjmoRE-3kn1OjpSeceKSLKgSeSDHOA1m7q278gPjRAThul8PTZYp7sZ02QpM57B9acrKFm1jcoKoEWdovyy03UNgRPbh_YVG3F73kK8fTQzqJEVJoW55E-CHpAUkLRwqhdaWf2R3dzypHveFVnqoJjMaFY3wAIpr8zn8Uj0iXVeCw3nj-bBcUh1L9haEr8PTnzrStBPII0yPxjU6AvR4JdceSE8SI5MfmlnlgTW-PgrXfTbIuznHJnxOHDkZTmqYwRPraaobhuRidky4sMOXVBRVLe1C2K7_13yc_LHLEJ1wwYC4FmNdUd8S2w9wyCC5jhNRt_4QInmEewo9DWIV3lUjhzqp272gdhfQUwqyVwEDl_eg9JAzFZYxRL56wIPEAA");
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            return View(db.asset.ToList());

            
        }

        // GET: Asset/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asset asset = db.asset.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: Asset/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nama,notes,barcodeId,type,remarks,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsActive")] asset asset)
        {
            if (ModelState.IsValid)
            {
                asset.SetPropertyCreate();
                db.asset.Add(asset);
                db.SaveChanges(); 
                SuccessMessagesAdd(Message.CreateSuccess);
                return RedirectToAction("Index");
            }

            return View(asset);
        }

        // GET: Asset/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asset asset = db.asset.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Asset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nama,notes,barcodeId,type,remarks,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsActive")] asset asset)
        {
            if (ModelState.IsValid)
            {
                asset.SetPropertyUpdate();
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                SuccessMessagesAdd(Message.UpdateSuccess);
                return RedirectToAction("Index");
            }
            return View(asset);
        }

        // GET: Asset/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asset asset = db.asset.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Asset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            asset asset = db.asset.Find(id);
            db.asset.Remove(asset);
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

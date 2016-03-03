using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Conference.Models;

namespace Conference.Controllers
{
    public class PresentationsController : Controller
    {

        private ConferenceContext db = new ConferenceContext();

        // GET: Presentations
        public ActionResult Index(string par)
        { ViewBag.PresenterCollection = (from p in db.Presenters
                                           select p).ToList();
            var ids = (from p in db.Presenters
                       where p.Name.Contains(par)
                       select p.Id).ToList();

            var model = from c in db.Presentations
                        orderby c.Pid
                        where ids.Contains(c.PresenterId)|| c.Title.Contains(par) || c.Kind.Contains(par) || par.Equals(null) || par.Equals("")
                        select c;

//c.PresenterId in ids ||

           
            return View(model);
        }


        // GET: Presentations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentation presentation = db.Presentations.Find(id);
            if (presentation == null)
            {
                return HttpNotFound();
            }
            return View(presentation);
        }

        // GET: Presentations/Create
        public ActionResult Create()
        {
            ViewBag.PresenterCollection = (from p in db.Presenters
                                           select p).ToList();

            return View();
        }

        // POST: Presentations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "Pid,Title,Abstract,PresenterId,Kind")] Presentation presentation)
        {
            if (ModelState.IsValid)
            {
                db.Presentations.Add(presentation);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(presentation);
        }

        // GET: Presentations/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.PresenterCollection = (from p in db.Presenters
                                           select p).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentation presentation = db.Presentations.Find(id);
            if (presentation == null)
            {
                return HttpNotFound();
            }
            //ViewBag.PresenterCollection = (from c in db.Presenters select c.Name).Distinct();
            return View(presentation);
        }

        // POST: Presentations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pid,Title,Abstract,PresenterId,Kind")] Presentation presentation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presentation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(presentation);
        }

        // GET: Presentations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentation presentation = db.Presentations.Find(id);
            if (presentation == null)
            {
                return HttpNotFound();
            }
            return View(presentation);
        }

        // POST: Presentations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Presentation presentation = db.Presentations.Find(id);
            db.Presentations.Remove(presentation);
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

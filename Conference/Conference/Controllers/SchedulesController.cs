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
    [CustomAuthorize(Roles.Admin, Roles.Palestrante, Roles.Usuario)]
    public class SchedulesController : Controller
    {
        private ConferenceContext db = new ConferenceContext();

        // GET: Schedules
        [CustomAuthorize(Roles.Admin, Roles.Palestrante, Roles.Usuario)]
        public ActionResult Index(string word)
        { ViewBag.Presentation = (from p in db.Presentations
                                               select p).ToList();
                var ids = (from p in db.Presentations
                           where p.Title.Contains(word)
                           select p.Pid).ToList();

                var model = from c in db.Schedules
                            orderby c.IdSchedule
                            where ids.Contains(c.Pid) || c.DateHour.ToString().Contains(word)
                            || word.Equals(null) || word.Equals("")
                            select c;

                //var model = from c in db.Schedules
                //        orderby c.DateHour
                //        where c.Host.Contains(word)
                //        || c.DateHour.ToString().Contains(word)               
                //        || word.Equals(null) || word.Equals("")
                        
                //        select c;
            

            return View(model);
        }

        // GET: Schedules/Details/5
        [CustomAuthorize(Roles.Admin, Roles.Palestrante, Roles.Usuario)]
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedules/Create
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Create()
        {
            ViewBag.PresenterCollection = (from c in db.Presentations select c.Title).Distinct();
            ViewBag.Presentation = db.Presentations.ToList();
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Create([Bind(Include = "IdSchedule,DateHour,Host,Pid")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedule);
        }

        // GET: Schedules/Edit/5
        [CustomAuthorize(Roles.Admin, Roles.Palestrante, Roles.Usuario)]
        public ActionResult Edit(int? id)
        {
            ViewBag.Presentation = (from p in db.Presentations
                                    select p).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Edit([Bind(Include = "IdSchedule,DateHour,Host,Pid")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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

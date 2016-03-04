using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Conference.Models;
using System.Web.Security;

namespace Conference.Controllers
{
    [CustomAuthorize(Roles.Admin)]
    public class UsersController : Controller
    {
        private ConferenceContext db = new ConferenceContext();

        // GET: Users   
        public ActionResult Index(string Username, string Email, int? kind, string atual)
        {
            
            var model = from c in db.Users
                        where c.Kind == kind
                        || kind.Equals(null)
                        select c;

            if (!String.IsNullOrEmpty(Username))
            {
                model = model.Where(s => s.Username.Contains(Username));
            }
            if (!String.IsNullOrEmpty(Email))
            {
                model = model.Where(s => s.Email.Contains(Email));
            }
            
            return View(model);
        }

        // GET: Users/Details/5
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Create()
        {
            ViewBag.opcoes = new List<SelectListItem>() { new SelectListItem { Text = "Admin", Value = "0" },
                                                          new SelectListItem { Text = "Palestrante", Value = "1" },
                                                          new SelectListItem { Text = "Usuário", Value = "2" }};

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Create([Bind(Include = "Username,Email,Password,Kind")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        [CustomAuthorize(Roles.Admin)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles.Admin)]
        public ActionResult Edit([Bind(Include = "Username,Email,Password,Kind")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [CustomAuthorize(Roles.Admin)]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles.Admin)]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        public ActionResult LoadUsers(string id) {
            var users = db.Users.Where(p => p.Username == id).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }




    }
}

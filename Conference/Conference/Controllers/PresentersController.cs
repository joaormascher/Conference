using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Conference.Models;

namespace Conference.Controllers {

    [CustomAuthorize(Roles.Admin, Roles.Palestrante, Roles.Usuario)]
    public class PresentersController : Controller
    {
        private ConferenceContext db = new ConferenceContext();

        // GET: Presenters
        [CustomAuthorize(Roles.Admin, Roles.Palestrante, Roles.Usuario)]
        public ActionResult Index(string find)
        {
            var model = from c in db.Presenters
                        orderby c.Name
                        where c.Name.Contains(find) || c.Title.Contains(find) || string.IsNullOrEmpty(find) //verificação da string nula ou vazia
                        select c;
            return View(model);
        }


        // GET: Presenters/Details/5
        [CustomAuthorize(Roles.Admin, Roles.Palestrante, Roles.Usuario)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presenter presenter = db.Presenters.Find(id);
            if (presenter == null)
            {
                return HttpNotFound();
            }
            return View(presenter);
        }

        // GET: Presenters/Create
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Create()
        {
            ViewBag.UsersCollection = db.Users.Select(p => new SelectListItem() {
                Text = p.Username,
                Value = p.Username.ToString()
            }).ToList();
            return View();
        }

        // POST: Presenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Title,Biography,UserOwner")] Presenter presenter)
        {
            if (ModelState.IsValid)
            {
                db.Presenters.Add(presenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(presenter);
        }

        // GET: Presenters/Edit/5
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presenter presenter = db.Presenters.Find(id);
            if (presenter == null)
            {
                return HttpNotFound();
            }
            return View(presenter);
        }

        // POST: Presenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Title,Biography,UserOwner")] Presenter presenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(presenter);
        }

        // GET: Presenters/Delete/5
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presenter presenter = db.Presenters.Find(id);
            if (presenter == null)
            {
                return HttpNotFound();
            }
            return View(presenter);
        }

        // POST: Presenters/Delete/5
        [CustomAuthorize(Roles.Admin, Roles.Palestrante)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Presenter presenter = db.Presenters.Find(id);
            db.Presenters.Remove(presenter);
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

        // [HttpPost]
        public ActionResult LoadPresenters(int? id)
        {
            var presenter = db.Presenters.Where(p => p.Id == id).ToList();
            return Json(presenter, JsonRequestBehavior.AllowGet);
        }
    }
}

﻿using Conference.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Conference.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(User u)
        {
            if (ModelState.IsValid)
            {
                using (ConferenceContext dc = new ConferenceContext())
                {
                    var v = dc.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if(v!=null)
                    {
                        Session["User"] = v;

                        CreateAuthorizeTicket(u.Username, "admin");

                        return RedirectToAction("Index");

                    }

                } 
            }

            return View();

        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return View("Index");
        }
        public void CreateAuthorizeTicket(string userId, string roles)
        {

            var authTicket = new FormsAuthenticationTicket(
              1,
              userId,  // Id do usuário é muito importante
              DateTime.Now,
              DateTime.Now.AddMinutes(30),  // validade 30 min tá bom demais
              false,   // Se você deixar true, o cookie ficará no PC do usuário
              roles);

            string encrypetedTicket = FormsAuthentication.Encrypt(authTicket);
            FormsAuthentication.SetAuthCookie(encrypetedTicket, false);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            Response.Cookies.Add(cookie);

        }

    }
}
using Innovation4Austria.logic;
using Innovation4Austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Verwaltung;

namespace Innovation4Austria.web.Controllers
{
    public class BenutzerController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Benutzer
        [HttpGet]
        public ActionResult Login()
        {
            log.Info("BenutzerController - Login - HttpGet");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]    
        public ActionResult Login(LoginModel model)
        {           
            log.Info("BenutzerController - Login - HttpPost");
            if (ModelState.IsValid)
            {
                if (model.Emailadresse != null)
                {
                    if (model.Passwort!=null)
                    {
                       if(BenutzerVerwaltung.Anmelden(model.Emailadresse, model.Passwort))
                        {
                            FormsAuthentication.SetAuthCookie(model.Emailadresse,true);
                            RedirectToRoute("http://www.w3schools.com/bootstrap/bootstrap_ref_css_text.asp");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(model.Emailadresse, false);
                        }
                    }
                }
            }
            return View(model);
        }
        /// <summary>
        /// Shows hole stuff of a company, their booked rooms and receipts of the company
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Dashboard(LoginModel model)
        {
            log.Info("BenutzerController - Dashboard");
            List<Benutzer> stuffOfCompany = BenutzerVerwaltung.LoadStuffOfACompany(model.Emailadresse);
            List<Buchung> bookingsOfCompany = ;
            
            if (stuffOfCompany != null)
            {

            }
            log.Warn("No stuff was found");
            return View();
        }
    }
}
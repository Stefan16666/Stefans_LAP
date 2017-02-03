using innovation4austria.authentication;
using Innovation4austria.authentication;
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

        private static readonly i4aMembershipProvider membershipProvider = new i4aMembershipProvider();

        private static readonly i4aRoleProvider roleProvider = new i4aRoleProvider();

        // GET: Benutzer
        [HttpGet]
        public ActionResult Login()
        {
            log.Info("BenutzerController - Login - HttpGet");
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            log.Info("BenutzerController - Login - HttpPost");
            if (ModelState.IsValid)
            {
                if (model.Emailadresse != null)
                {
                    if (model.Passwort != null)
                    {
                        var logonResult = BenutzerVerwaltung.Anmelden(model.Emailadresse, model.Passwort);
                        if (membershipProvider.ValidateUser(model.Emailadresse, model.Passwort))
                        {                            
                            if (roleProvider.IsUserInRole(model.Emailadresse, "MitarbeiterIVA"))
                            {
                                return RedirectToAction("FirmenWahl");
                            }
                            FormsAuthentication.SetAuthCookie(model.Emailadresse, true);
                            Firma company = BenutzerVerwaltung.LadeFirmaVonBenutzer(model.Emailadresse);
                            {
                                if (company == null)
                                {
                                    log.Debug("keine Firma gefunden");
                                }
                            }
                        }
                        return RedirectToAction("Dashboard", model);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.Emailadresse, false);
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
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Dashboard(int fa_id)
        {
            log.Info("BenutzerController - Dashboard");
            // mapping for user
            List<Benutzer> stuffOfCompany = BenutzerVerwaltung.LoadStuffOfACompany(fa_id);

            // mapping for bookings
            if (stuffOfCompany != null)
            {
                List<Buchung> bookingsOfCompany = RaumVerwaltung.BookedRooms(fa_id);
                if (bookingsOfCompany != null)
                {
                    foreach (var booking in bookingsOfCompany)
                    {
                        DateTime min;
                        DateTime max;

                        min = (from d1 in booking.AlleBuchungsdetails orderby d1.Datum select d1.Datum).FirstOrDefault();
                        max = (from d1 in booking.AlleBuchungsdetails orderby d1.Datum descending select d1.Datum).FirstOrDefault();

                        string roomName = booking.Raum.Bezeichnung;
                    }
                }
                log.Warn("No stuff was found");


            }

            ///mapping for receipt
            List<Rechnung> alleRechungen = null;

            return View();
        }
        [HttpGet]
        public ActionResult FirmenWahl()
        {
            List<Firma> alleFirmen = BenutzerVerwaltung.LadeAlleFirmen();
            if (alleFirmen.Count <= 0)
            {
                log.Error("BenutzerController - FirmenWahl - keine Firmengefunden");
            }
            return View(alleFirmen);
        }
    }
}
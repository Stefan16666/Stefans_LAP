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
                                else
                                {
                                    return RedirectToAction("Dashboard", new { id = company.Id });
                                }
                            }
                        }
                       
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
        public ActionResult Dashboard(int id)
        {
            log.Info("BenutzerController - Dashboard");
            // mapping for user
            DashboardModel dashboard = new DashboardModel();
            List<MitarbeiterModel> alleMitarbeitereinerFirma = new List<MitarbeiterModel>();
            List<Benutzer> alleBenutzer = BenutzerVerwaltung.LoadStuffOfACompany(id);
            foreach (var einBenutzer in alleBenutzer)
            {
                MitarbeiterModel einMitarbeiter = new MitarbeiterModel
                {
                    emailadresse = einBenutzer.Emailadresse,
                    nachname = einBenutzer.Nachname
                };
                alleMitarbeitereinerFirma.Add(einMitarbeiter);
            }
            dashboard.alleMitarbeiter = alleMitarbeitereinerFirma;


            // mapping for bookings
            if (alleBenutzer != null)
            {
                Rechnungsdetails detail = null;
                List<Rechnungsdetails>rechnungsDetailsEinerBuchung = new List<Rechnungsdetails>();
                List<Buchungsdetails> BuchungsDetailsVonFirma = null;
                List<Buchung> bookingsOfCompany = RaumVerwaltung.GebuchteRaeume(id);
                if (bookingsOfCompany != null)
                {
                    foreach (var booking in bookingsOfCompany)
                    {
                        DateTime min;
                        DateTime max;

                        min = (from d1 in booking.AlleBuchungsdetails orderby d1.Datum select d1.Datum).FirstOrDefault();
                        max = (from d1 in booking.AlleBuchungsdetails orderby d1.Datum descending select d1.Datum).FirstOrDefault();

                        string roomName = booking.Raum.Bezeichnung;

                        BuchungsDetailsVonFirma = RaumVerwaltung.BuchungsDetailsVonBuchung(booking.Id);
                    }
                    foreach (var buchungsdetail in BuchungsDetailsVonFirma)
                    {
                        if (RechnungsVerwaltung.EinRechnungsDetailsEinerBuchung(buchungsdetail.Id) == null)
                        {
                            BuchungsDetailsVonFirma.Remove(buchungsdetail);
                        }
                        
                    }

                    /// hier muss man dann die Models machen. Das RechnungsModel ist eine Liste von Rechnungen, welche Monatsweise erstellt werden
                }
                log.Warn("No stuff was found");


            }

            ///mapping for receipt
            


            return View(dashboard);
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
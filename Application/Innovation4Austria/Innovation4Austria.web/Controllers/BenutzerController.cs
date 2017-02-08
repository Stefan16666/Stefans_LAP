
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
                                    model.Fa_id = company.Id;
                                    return RedirectToAction("Dashboard", model);
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
        public ActionResult Dashboard(LoginModel model)
        {
            log.Info("BenutzerController - Dashboard");
            // mapping for user
            DashboardModel dashboard = new DashboardModel();


            List<MitarbeiterModel> alleMitarbeitereinerFirma = new List<MitarbeiterModel>();
            List<Benutzer> alleBenutzer = BenutzerVerwaltung.LoadStuffOfACompany(model.Fa_id);
            foreach (var einBenutzer in alleBenutzer)
            {
                MitarbeiterModel einMitarbeiter = new MitarbeiterModel
                {
                    Emailadresse = einBenutzer.Emailadresse,
                    Nachname = einBenutzer.Nachname
                };
                alleMitarbeitereinerFirma.Add(einMitarbeiter);
            }
            dashboard.AlleMitarbeiter = alleMitarbeitereinerFirma;


            // mapping for bookings
            if (alleBenutzer != null)
            {
                BuchungenModel buchungsmodel = new BuchungenModel();
                List<BuchungenModel> alleBuchungen = new List<BuchungenModel>();
                List<Rechnungsdetails> rechnungsDetailsEinerBuchung = new List<Rechnungsdetails>();
                List<Buchungsdetails> BuchungsDetailsVonFirma = null;
                List<Buchung> bookingsOfCompany = RaumVerwaltung.GebuchteRaeume(model.Fa_id);
                if (bookingsOfCompany != null)
                {
                    /// es fehlt noch RaumArt und RaumName
                    foreach (var booking in bookingsOfCompany)
                    {
                        Buchungsdetails buchungsdetail = RaumVerwaltung.BuchungsDetailsVonBuchung(booking.Id);
                        BuchungsDetailsVonFirma.Add(buchungsdetail);
                        Raum aktRaum = 
                        buchungsmodel.Raumnummer = booking.Raum_id.ToString();
                        buchungsmodel.RaumArt = booking
                        foreach (var einbuchungsDetail in BuchungsDetailsVonFirma)
                        {
                            buchungsmodel.VonDatum = BuchungsVerwaltung.datum(einbuchungsDetail.Id,true);
                            buchungsmodel.BisDatum = BuchungsVerwaltung.datum(einbuchungsDetail.Id, false);
                        }
                        string roomName = booking.Raum.Bezeichnung;

                        
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
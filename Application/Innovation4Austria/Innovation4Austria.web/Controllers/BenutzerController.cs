
using innovation4austria.authentication;
using innovation4austria.logic;
using Innovation4austria.authentication;
using Innovation4Austria.logic;
using Innovation4Austria.web.AppCode;
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
                            FormsAuthentication.SetAuthCookie(model.Emailadresse, true);
                            if (roleProvider.IsUserInRole(model.Emailadresse, "MitarbeiterIVA"))
                            {
                                return RedirectToAction("FirmenWahl");
                            }
                            TempData[ConstStrings.SUCCESS_MESSAGE] = Validierungen.SpeichernErfolgreich;
                            Benutzer user = BenutzerVerwaltung.SucheFirmaVonBenutzer(model.Emailadresse);
                            if (user != null)
                            {
                                if (user.Firma_id != 0)
                                {
                                    model.Fa_id = (int)user.Firma_id;
                                }

                                return RedirectToAction("Dashboard", model);
                            }
                        }
                        else
                        {
                            return RedirectToAction("Login");
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
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Dashboard(LoginModel model)
        {
            log.Info("BenutzerController - Dashboard");

            DashboardModel dashboard = new DashboardModel();

            // holt alle Mitarbeiter einer Firma
            List<BenutzerModel> alleMitarbeitereinerFirma = new List<BenutzerModel>();
            List<Benutzer> alleBenutzer = BenutzerVerwaltung.LoadStuffOfACompany(model.Fa_id);
            if (alleBenutzer == null)
            {
                log.Warn("No stuff was found");
            }
            foreach (var einBenutzer in alleBenutzer)
            {
                BenutzerModel einMitarbeiter = new BenutzerModel()
                {
                    Emailadresse = einBenutzer.Emailadresse,
                    Nachname = einBenutzer.Nachname,
                    Vorname = einBenutzer.Vorname
                };
                alleMitarbeitereinerFirma.Add(einMitarbeiter);
            }
            dashboard.AlleMitarbeiter = alleMitarbeitereinerFirma;


            // mapping for bookings

            BuchungenModel buchungsmodel = new BuchungenModel();
            List<BuchungenModel> alleBuchungen = new List<BuchungenModel>();
            List<Rechnungsdetails> rechnungsDetailsEinerBuchung = new List<Rechnungsdetails>();
            List<Buchungsdetails> BuchungsDetailsVonFirma = new List<Buchungsdetails>();

            List<Buchung> bookingsOfCompany = BuchungsVerwaltung.GebuchteRaeume(model.Fa_id);

            if (bookingsOfCompany != null)
            {
                /// es fehlt noch RaumArt und RaumName
                foreach (var booking in bookingsOfCompany)
                {

                    BuchungsDetailsVonFirma = BuchungsVerwaltung.BuchungsDetailsVonBuchung(booking.Id);

                    Raum aktRaum = RaumVerwaltung.GesuchterRaum(booking.Raum_id);
                    buchungsmodel.Raumnummer = aktRaum.Bezeichnung;
                    buchungsmodel.RaumArt = aktRaum.Art.Bezeichnung;

                    foreach (var einbuchungsDetail in BuchungsDetailsVonFirma)
                    {
                        buchungsmodel.VonDatum = BuchungsVerwaltung.datum(einbuchungsDetail.Id, true);
                        buchungsmodel.BisDatum = BuchungsVerwaltung.datum(einbuchungsDetail.Id, false);
                    }
                    alleBuchungen.Add(buchungsmodel);

                }
            }
            else
            {
                log.Info("BenutzerController - Dashboard - keine Buchungen für die Firma vorhanden sind");
            }
            dashboard.AlleBuchungen = alleBuchungen;

            List<int> dates = new List<int>();

            foreach (var buchungsdetail in BuchungsDetailsVonFirma)
            {
                if (RechnungsVerwaltung.EinRechnungsDetailsEinesBuchungsDetails(buchungsdetail.Id) == null)
                {
                    BuchungsDetailsVonFirma.Remove(buchungsdetail);
                }
                //else
                //{
                //    RechnungsModel einRgModel = new RechnungsModel();
                //    Rechnungsdetails detail = RechnungsVerwaltung.EinRechnungsDetailsEinesBuchungsDetails(buchungsdetail.Id);
                //    einRgModel.Rechnungsnummer = detail.Rechnung_Id;
                //    einRgModel.Monat = "sss";
                //}

                if (!dates.Contains(BuchungsVerwaltung.datum(buchungsdetail.Id, true).Month))
                {
                    int date = BuchungsVerwaltung.datum(buchungsdetail.Id, true).Month;
                    dates.Add(date);
                }
            }
            List<RechnungsModel> alleRechnungen = new List<RechnungsModel>();

            foreach (var item in dates)
            {
                RechnungsModel RgModel = new RechnungsModel()
                {
                    Monat = Monat(item)
                };
                alleRechnungen.Add(RgModel);

            };

            dashboard.AlleRechnungen = alleRechnungen;
            /// hier muss man dann die Models machen. Das RechnungsModel ist eine Liste von Rechnungen, welche Monatsweise erstellt werden

            ///mapping for receipt



            return View(dashboard);
        }


        [Authorize]
        [HttpGet]
        public ActionResult Abmelden()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Benutzer");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfilAnzeigen()
        {
            log.Info("GET - User - ProfileData()");

            Benutzer aktBenutzer = BenutzerAdministrator.GetUser(User.Identity.Name);

            ProfilAnzeigeModel profilModel = new ProfilAnzeigeModel();

            




            return View();

        }
        public static string Monat(int i)
        {
            string monat = "";
            if (i == 1)
            {
                monat = "Januar";
            }
            else if (i == 2)
            {
                monat = "Februar";
            }
            else if (i == 3)
            {
                monat = "März";
            }
            else if (i == 4)
            {
                monat = "April";
            }
            else if (i == 5)
            {
                monat = "Mai";
            }
            else if (i == 6)
            {
                monat = "Juni";
            }
            else if (i == 7)
            {
                monat = "Juli";
            }
            else if (i == 8)
            {
                monat = "August";
            }
            else if (i == 9)
            {
                monat = "September";
            }
            else if (i == 10)
            {
                monat = "Oktober";
            }
            else if (i == 11)
            {
                monat = "November";
            }
            else if (i == 12)
            {
                monat = "Dezember";
            }
            return monat;
        }

    }
}
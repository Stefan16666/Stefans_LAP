using Innovation4Austria.logic;
using Innovation4Austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Innovation4Austria.web.Controllers
{
    public class StornierungController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Stornierung
        [Authorize]
        [HttpGet]
        public ActionResult Anzeigen()
        {
            log.Info("StornierungController - Index - GET");
            List<StornierungsAnzeigeModel> alleStornierungen = ErstelleStornierungsliste();

            return View(alleStornierungen);
        }


        [Authorize]
        [HttpGet]
        public ActionResult Emailsenden()
        {
            log.Info("StornierungController - Index - GET");

            List<StornierungsAnzeigeModel> alleStornierungen = ErstelleStornierungsliste();
            bool StornolisteUebermittelt = Emailversenden(alleStornierungen);
            if (StornolisteUebermittelt)
            {
                return RedirectToAction("FirmenAuflistung", "Innovation4AustriaMitarbeiter");
            }
            return View("Anzeigen", alleStornierungen);
           
        }


        public static List<StornierungsAnzeigeModel> ErstelleStornierungsliste()
        {
            List<StornierungsAnzeigeModel> alleStornierungen = new List<StornierungsAnzeigeModel>();
            List<Buchung> alleBuchungen = new List<Buchung>();
            List<Firma> alleFirmen = FirmenVerwaltung.LadeAlleFirmen();
            foreach (var firma in alleFirmen)
            {
                StornierungsAnzeigeModel StornoZuEinerFirma = new StornierungsAnzeigeModel();
                List<Buchungsdetails> alleBuchungsDetails = new List<Buchungsdetails>();
                StornoZuEinerFirma.AnzahlStornierungen = 0;
                if (StornoZuEinerFirma.Firmenname == null)
                {
                    StornoZuEinerFirma.Firmenname = firma.Bezeichnung;
                }
                alleBuchungen = BuchungsVerwaltung.BuchungenZuFirmaDieStorniertWurden(firma.Id);
                foreach (var buchung in alleBuchungen)
                {
                    List<Buchungsdetails> alleDetailsEinerBuchung = new List<Buchungsdetails>();
                    alleDetailsEinerBuchung = BuchungsVerwaltung.HoleBuchungsDetails(buchung.Id);
                    foreach (var buchungsdetail in alleDetailsEinerBuchung)
                    {
                        StornoZuEinerFirma.AnzahlStornierungen++;

                    }
                }
                alleStornierungen.Add(StornoZuEinerFirma);

            }

            return alleStornierungen;
        }

        public static bool Emailversenden(List<StornierungsAnzeigeModel> alleStornierungen)
        {
            bool versendet = false;
            log.Info("StornierungController - Emailversenden");
            try
            {
                string from = "Stefan.groinig@qualifizierung.or.at";
                string passwort = "123user!";
                string to = "bbrz@wien.at";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("stefan.groinig@qualifizierung.or.at"); //Absender 
                mail.To.Add("stefan.groinig@outlook.com"); //Empfänger 

                mail.Subject = "Stornoliste vom letzten Monat";

                foreach (var item in alleStornierungen)
                {
                    mail.Body += item.Firmenname + " " + item.AnzahlStornierungen + Environment.NewLine;
                }


                SmtpClient client = new SmtpClient("srv08.itccn.loc", 25); //SMTP Server von Hotmail und Outlook. 


                client.Credentials = new NetworkCredential(from, passwort);//Anmeldedaten für den SMTP Server 

                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                //client.Host = "srv08.itccn.loc";

                //client.EnableSsl = true; //Die meisten Anbieter verlangen eine SSL-Verschlüsselung 

                client.Send(mail); //Senden 

                Console.WriteLine("E-Mail wurde versendet");

                versendet = true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Fehler beim Senden der E-Mail\n\n{0}", ex.Message);
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StatusCode);
            }
          

            return versendet;
        }
    }
}
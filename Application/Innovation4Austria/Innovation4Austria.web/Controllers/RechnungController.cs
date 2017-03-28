using Innovation4Austria.logic;
using Innovation4Austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation4Austria.web.Controllers
{
    public class RechnungController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Authorize]
        [HttpGet]
        public ActionResult Erstellen(int Monatnummer)
        {
            log.Info("Rechnung - Erstellen - GET");

            Benutzer aktBenutzer = BenutzerAdministrator.GetUser(User.Identity.Name);

            List<Buchungsdetails> gesuchteBuchungsDetails = RechnungsVerwaltung.MonatsBuchungsDetails((int)aktBenutzer.Firma_id, Monatnummer);

            Rechnung rechnung = RechnungsVerwaltung.LiefereRechnungzuFirmaAusMonat(gesuchteBuchungsDetails);

            RechnungErstellenModel rechnungsModel = new RechnungErstellenModel();

            Firma firma = FirmenVerwaltung.LiefereFirma((int)aktBenutzer.Firma_id);

            rechnungsModel.Rg_Id = rechnung.Id;
            rechnungsModel.Firmenname = firma.Bezeichnung;
            rechnungsModel.FirmenstrassenNummer = firma.Nummer;
            rechnungsModel.FirmenOrt = firma.Ort;
            rechnungsModel.Firmenstrasse = firma.Strasse;
            rechnungsModel.FirmenPlz = firma.Plz;
            rechnungsModel.Datum = (DateTime)rechnung.Datum;
            rechnungsModel.Firmenname = firma.Bezeichnung;
            rechnungsModel.Gesamtpreis = gesuchteBuchungsDetails.Sum(x => x.Preis);
            rechnungsModel.Steuerbetrag = (rechnungsModel.Gesamtpreis / 100) * 20;
            rechnungsModel.RechnungsDetails = new List<RechnungsDetailModel>();
            foreach (var buchungsDetail in gesuchteBuchungsDetails)
            {
                RechnungsDetailModel model = new RechnungsDetailModel()
                {
                    Buchungs_ID = buchungsDetail.Id.ToString(),
                    Buchungsdatum = buchungsDetail.Datum,
                    Preis = buchungsDetail.Preis,
                    RaumNummer = RaumVerwaltung.GesuchterRaumName(buchungsDetail.Buchung_id)
                };

                rechnungsModel.RechnungsDetails.Add(model);
            }
            DateTime VonDatum = (from x in gesuchteBuchungsDetails orderby x.Datum select x.Datum).FirstOrDefault();
            DateTime BisDatum = (from x in gesuchteBuchungsDetails orderby x.Datum descending select x.Datum).FirstOrDefault();

            return new Rotativa.ViewAsPdf("Erstellen", rechnungsModel);
        }
    }
}

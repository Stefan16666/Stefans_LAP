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

            //List<Buchungsdetails> BuchungsdetailsVonEinemMonat = BuchungsVerwaltung.BuchungsDetailsVonMonat(Monatnummer);

            //List<Rechnung> AlleRechnungen = new List<Rechnung>();

            ////List<Rechnungsdetails> RechnungsDetailsZuEinerFirma = new List<Rechnungsdetails>();

            //List<Rechnungsdetails> RechnungsDetailsZuEinemBuchungsDetail = new List<Rechnungsdetails>();            

            //foreach (var buchungsDetail in BuchungsdetailsVonEinemMonat)
            //{
            //    Rechnungsdetails einRechnungsDetail = RechnungsVerwaltung.EinRechnungsDetailsEinesBuchungsDetails(buchungsDetail.Id);
            //    RechnungsDetailsZuEinemBuchungsDetail.Add(einRechnungsDetail);
            //}
            //foreach (var rechnungsDetail  in RechnungsDetailsZuEinemBuchungsDetail)
            //{               
            //        Rechnung MonatsRechnung = RechnungsVerwaltung.MonatsRechnung(rechnungsDetail.Rechnung_Id);
            //    AlleRechnungen.Add(MonatsRechnung);                
            //}

            //Rechnung gesuchteRechnung = AlleRechnungen.Where(x => x.fa_id == (int)aktBenutzer.Firma_id).FirstOrDefault();

            //List<Rechnungsdetails> RechnungsDetailsZuRechnung = RechnungsVerwaltung.RechnungsDetailsEinerRechnung(gesuchteRechnung.Id);

            List<Buchungsdetails> gesuchteBuchungsDetails = RechnungsVerwaltung.MonatsBuchungsDetails((int)aktBenutzer.Firma_id, Monatnummer);
            //foreach (var rechnungsDetail in RechnungsDetailsZuRechnung)
            //{
            //    Buchungsdetails BuchungsDetailZuRechnungsDetail = RechnungsVerwaltung.BuchungsDetailEinerRechnung(rechnungsDetail.Buchungsdetail_Id);
            //    gesuchteBuchungsDetails.Add(BuchungsDetailZuRechnungsDetail);
            //}


            RechnungErstellenModel rechnungsModel = new RechnungErstellenModel();

            return View(gesuchteBuchungsDetails);
        }
    }
}
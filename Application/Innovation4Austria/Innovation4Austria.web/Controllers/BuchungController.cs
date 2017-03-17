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
    public class BuchungController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        public ActionResult BuchungsDetails(DateTime StartDatum, DateTime EndDatum, string datumVonBis, int Id)
        {
            log.Info("Buchung - BuchungsDetails - Get");



            BuchungsDetailModel mapBuchungsDetails = AutoMapper.Mapper.Map<BuchungsDetailModel>(RaumVerwaltung.GesuchterRaum(Id));

            List<Raum_Ausstattung> raumAusstattung = RaumVerwaltung.RaumAusstattungEinesRaumes(Id);

            List<RaumAusstattungsFilterModel> mapRaumAusstattung = AutoMapper.Mapper.Map<List<RaumAusstattungsFilterModel>>(RaumVerwaltung.RaumAusstattungEinesRaumes(Id));

            BuchungsDetailModel buchungsDetails = new BuchungsDetailModel();
            buchungsDetails = mapBuchungsDetails;
            buchungsDetails.RaumAusstattung = new List<RaumAusstattungsFilterModel>();
            buchungsDetails.RaumAusstattung = mapRaumAusstattung;
            
            buchungsDetails.VonDatum = StartDatum;
            buchungsDetails.BisDatum = EndDatum;

            return View(buchungsDetails);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Raumbuchung(int Id, DateTime VonDatum, DateTime BisDatum, decimal Preis)
        {

            return RedirectToAction("Laden", "Raum");

        }


    }
}
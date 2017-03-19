using Innovation4Austria.logic;
using Innovation4Austria.web.AppCode;
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

        [Authorize]
        [HttpPost]
        public ActionResult BuchungsDetails(DateTime StartDatum, DateTime EndDatum, string datumVonBis, int Id)
        {
            log.Info("Buchung - BuchungsDetails - Get");


            BuchungsDetailModel buchungsDetails = new BuchungsDetailModel();
            buchungsDetails = AutoMapper.Mapper.Map<BuchungsDetailModel>(RaumVerwaltung.GesuchterRaum(Id));
            buchungsDetails.RaumAusstattung = new List<RaumAusstattungsFilterModel>();

            List<Raum_Ausstattung> raumAusstattung = RaumVerwaltung.RaumAusstattungEinesRaumes(Id);
            buchungsDetails.RaumAusstattung = new List<RaumAusstattungsFilterModel>();

            foreach (var item in raumAusstattung)
            {
                RaumAusstattungsFilterModel model = new RaumAusstattungsFilterModel()
                {
                    Bezeichnung = item.Ausstattung.Bezeichnung,
                    Ausstattungs_Id = item.Ausstattungs_Id
                };
                buchungsDetails.RaumAusstattung.Add(model);
            }

            //buchungsDetails.RaumAusstattung = AutoMapper.Mapper.Map<List<RaumAusstattungsFilterModel>>(RaumVerwaltung.RaumAusstattungEinesRaumes(Id));

            List<FirmenAusWahlModel> alleFirmen = AutoMapper.Mapper.Map<List<FirmenAusWahlModel>>(FirmenVerwaltung.LadeAlleFirmen());
            
            //buchungsDetails.RaumAusstattung = mapRaumAusstattung;
            buchungsDetails.Firma = alleFirmen;
            
            buchungsDetails.VonDatum = StartDatum;
            buchungsDetails.BisDatum = EndDatum;

            return View(buchungsDetails);
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult Raumbuchung(int Id, DateTime VonDatum, DateTime BisDatum, decimal Preis, int? Fa_Id)
        {
            bool gebucht = false;
            log.Info("BuchnungController - Raumbuchung - POST");
            int fa_id = 0;
            if (Fa_Id == null)
            {
                Benutzer aktBenutzer = BenutzerAdministrator.GetUser(User.Identity.Name);
                fa_id = (int)aktBenutzer.Firma_id;
            }

            VerbindlichBuchenModel verbindlichBuchenModel = new VerbindlichBuchenModel();
            
            if (Id >= 0)
            {
                verbindlichBuchenModel.buchung_id = BuchungsVerwaltung.ErstelleBuchung(Id,fa_id);
            }

            if (verbindlichBuchenModel.buchung_id >=0)
            {
                if (gebucht = BuchungsVerwaltung.ErstelleBuchungDetails(verbindlichBuchenModel.buchung_id, VonDatum, BisDatum, Preis))
                {
                    TempData[ConstStrings.SUCCESS_MESSAGE] = Validierungen.BuchenErfolgreich;
                }
                else
                {
                    TempData[ConstStrings.ERROR_MESSAGE] = Validierungen.BuchenFehlgeschlagen;
                }
                
            }

            return RedirectToAction("Laden", "Raum");
        }


    }
}
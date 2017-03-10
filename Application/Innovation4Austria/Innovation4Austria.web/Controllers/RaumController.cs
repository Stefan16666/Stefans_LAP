using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Innovation4Austria.web.Models;
using Innovation4Austria.logic;
using Innovation4Austria.web.AppCode;

namespace Innovation4Austria.web.Controllers
{
    public class RaumController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        [Authorize]
        [HttpGet]
        public ActionResult Laden()
        {

            log.Info("RaumController - Laden - GET");

            FilterModel filterModel = new FilterModel();

            filterModel.Ausstattung = AutoMapper.Mapper.Map<List<RaumAusstattungsModel>>(RaumVerwaltung.AlleRaumAusstattungen());
            filterModel.Art = AutoMapper.Mapper.Map<List<RaumArtModel>>(RaumVerwaltung.AlleRaumArten());
            filterModel.AlleRaeume = AutoMapper.Mapper.Map<List<RaumModel>>(RaumVerwaltung.alleRaeume());

            return View(filterModel);
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult Suchen(string datumVonBis, int Art_id, int[] ausstattung)
        {

            log.Info("RaumController - Suchen - Post");
            List<RaumModel> gesuchteRaeume = new List<RaumModel>();
            if (ModelState.IsValid)
            {

                try
                {
                    //datumVonBis = "04/12/2017 - 04/21/2017"

                    // VON
                    // "04/12/2017"
                    string von = datumVonBis.Substring(0, 10);

                    // aufteilen des strings in Datumsbestandteile
                    int vonMonat = int.Parse(von.Substring(0, 2));
                    int vonTag = int.Parse(von.Substring(3, 2));
                    int vonJahr = int.Parse(von.Substring(6, 4));

                    // erstelle datetime
                    DateTime anfangsDatum = new DateTime(vonJahr, vonMonat, vonTag);

                    // BIS
                    // "13/12/2017"
                    string bis = datumVonBis.Substring(13, 10);

                    // aufteilen des strings in Datumsbestandteile
                    int bisMonat = int.Parse(bis.Substring(0, 2));
                    int bisTag = int.Parse(bis.Substring(3, 2));
                    int bisJahr = int.Parse(bis.Substring(6, 4));

                    // erstelle datetime
                    DateTime endDatum = new DateTime(bisJahr, bisMonat, bisTag);

                    gesuchteRaeume = AutoMapper.Mapper.Map<List<RaumModel>>(RaumVerwaltung.GesuchteRaeume(anfangsDatum, endDatum, Art_id, ausstattung));

                    //endDatum.Subtract(anfangsDatum).TotalDays
                }
                catch (Exception ex)
                {
                    TempData[ConstStrings.ERROR_MESSAGE] = Validierungen.SuchenFehlgeschlagen;
                    log.Error("RaumController - Suchen - Post - Suchen fehlgeschlagen");

                }
            }

            return View("_KartenAnsicht", gesuchteRaeume);
        }

        [HttpPost]
        public ActionResult RaumAnzeigen(FilterModel model)
        {

            return View();
        }
    }
}
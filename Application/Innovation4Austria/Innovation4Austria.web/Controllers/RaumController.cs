using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Innovation4Austria.web.Models;
using Innovation4Austria.logic;

namespace Innovation4Austria.web.Controllers
{
    public class RaumController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        [HttpGet]
        public ActionResult Suchen(DateTime startdatum, DateTime enddatum, int raum_id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult RaumAuflistung(FilterModel model)
        {
            return View();
        }
    }
}
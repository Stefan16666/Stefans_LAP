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
        public ActionResult BuchungsDetails(DateTime StartDatum, DateTime EndDatum,int Id)
        {
            log.Info("Buchung - BuchungsDetails - Get");
            BuchungsDetailModel buchungsDetails = new BuchungsDetailModel();
            buchungsDetails = AutoMapper.Mapper.Map<BuchungsDetailModel>(RaumVerwaltung.GesuchterRaum(Id));
            buchungsDetails.RaumAusstattung = new List<RaumAusstattungsModel>();
            buchungsDetails.RaumAusstattung = AutoMapper.Mapper.Map<List<RaumAusstattungsModel>>(RaumVerwaltung.AlleRaumAusstattungen());
            return View();
        }
    }
}
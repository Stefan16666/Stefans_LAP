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
        // GET: Rechnung
        public ActionResult Erstellen(int Monatnummer)
        {
            log.Info("Rechnung - Erstellen - GET");

            RechnungErstellenModel rechnungsModel = new RechnungErstellenModel();

            return View();
        }
    }
}
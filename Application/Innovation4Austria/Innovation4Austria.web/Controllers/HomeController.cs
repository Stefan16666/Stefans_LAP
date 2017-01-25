using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using log4net.Config;
using Innovation4Austria.logic;

namespace Innovation4Austria.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /// die Einstellungen aus demn Config-File
            /// sollen für log4net übernommen werden!
           

            ///erzeuge einen Log-Manager
            /// Name beliebig - sinnvoll wäre natürlich ein sprechender zB. Klassen-Name
            ILog log = LogManager.GetLogger("Hallo Welt");

            /// Protokolliere einen Eintrag vom Level
            ///     Debug
            ///     Info
            ///     Warn
            ///     Error

            log.Debug("Debug");

            log.Info("Info");

            log.Warn("Warn");

            log.Error("Error");

            // Fake - Aufrufe um Protokolleierung aus diesen Methoden einzusehene

            BenutzerVerwaltung.Laden();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
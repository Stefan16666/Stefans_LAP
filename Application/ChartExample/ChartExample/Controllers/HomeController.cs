using ChartExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChartExample.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index()
        {
            /// fiktiv - Abfrage der BusinessLogic um die Daten zu ermitteln
            /// (hier werden die Daten statisch zu Testzwecken erstellt)
            #region Fake-Daten erstellen
            List<MonatsModel> model = new List<MonatsModel>();
            model.Add(new MonatsModel()
            {
                Monat = "Jänner",
                Buchungen = 123
            });
            model.Add(new MonatsModel()
            {
                Monat = "Mai",
                Buchungen = 222
            });
            model.Add(new MonatsModel()
            {
                Monat = "Juli",
                Buchungen = 512
            });
            #endregion  

            return View(model);
        }
    }
}
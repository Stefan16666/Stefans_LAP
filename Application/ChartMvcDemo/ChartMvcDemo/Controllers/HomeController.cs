using ChartMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChartMvcDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LinienDiagramm()
        {
            /// fiktiv - Abfrage der BusinessLogic um die Daten zu ermitteln
            /// (hier werden die Daten statisch zu Testzwecken erstellt)
            #region Fake-Daten erstellen
            List<UmsatzModel> model = new List<UmsatzModel>();
            model.Add(new UmsatzModel()
            {
                Monat = "Jänner",
                Umsatz = 123
            });
            model.Add(new UmsatzModel()
            {
                Monat = "Mai",
                Umsatz = 222
            });
            model.Add(new UmsatzModel()
            {
                Monat = "Juli",
                Umsatz = 512
            });
            #endregion  

            return View(model);
        }
    }
}
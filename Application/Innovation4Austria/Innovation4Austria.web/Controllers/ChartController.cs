using Innovation4Austria.logic;
using Innovation4Austria.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Innovation4Austria.web.Controllers
{


    public class ChartController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {

            List<MonatsModel> alleModel = new List<MonatsModel>();

            List<Firma> alleFirmen = new List<Firma>();

            alleFirmen = FirmenVerwaltung.LadeAlleFirmen();
            List<Buchung> alleBuchungen = new List<Buchung>();

            //List<Buchungsdetails> alleBuchungsDetails = new List<Buchungsdetails>();
            foreach (var firma in alleFirmen)
            {
                MonatsModel firmenModel = new MonatsModel();
                List<Buchungsdetails> alleBuchungsDetails = new List<Buchungsdetails>();
                firmenModel.Umsatz = 0;
                if (firmenModel.Firmenbezeichnung == null)
                {
                    firmenModel.Firmenbezeichnung = firma.Bezeichnung;
                }
                alleBuchungen = BuchungsVerwaltung.GebuchteRaeume(firma.Id);
                foreach (var buchung in alleBuchungen)
                {
                    List<Buchungsdetails> alleDetailsEinerBuchung = new List<Buchungsdetails>();
                    alleDetailsEinerBuchung = BuchungsVerwaltung.HoleBuchungsDetails(buchung.Id);                
                    foreach (var buchungsdetail in alleDetailsEinerBuchung)
                    {
                        decimal preis = buchungsdetail.Preis;
                        firmenModel.Umsatz += preis;
                    }
                }
                alleModel.Add(firmenModel);

            }

            return View(alleModel);
        }
    }
}

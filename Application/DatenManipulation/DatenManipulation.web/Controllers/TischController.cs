using DatenManipulation.logic;
using DatenManipulation.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatenManipulation.web.Controllers
{
    public class TischController : Controller
    {
        // GET: Tisch
        [HttpGet]
        public ActionResult Index()
        {
            TischAnlegenModel neuerTisch = new TischAnlegenModel();
            List<RaumModel> RaumZuTisch = new List<RaumModel>();
            List<Raum> BlRaueme = TischVerwaltung.alleRaeume();
            foreach (var Raum in BlRaueme)
            {
                RaumZuTisch.Add(new RaumModel()
                {
                    Bezeichnung = Raum.Bezeichnung,
                    ID = Raum.ID
                });
            }
            neuerTisch.alleRaume = RaumZuTisch;
            return View(neuerTisch);
        }
    }
}
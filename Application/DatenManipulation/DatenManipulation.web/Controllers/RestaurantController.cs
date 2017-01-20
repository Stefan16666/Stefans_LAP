//using DatenManipulation.logic;
using DatenManipulation.logic;
using DatenManipulation.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DatenManipulation.web.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Anlegen()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Anlegen(ModelRaum model)
        {
            if (ModelState.IsValid)
            {
                bool erfolgreich = RaumVerwaltung.Anlegen(model.Bezeichnung);
                if (erfolgreich)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
    }
}
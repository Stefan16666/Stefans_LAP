using Innovation4austria.authentication;
using Innovation4Austria.authentication;
using Innovation4Austria.logic;
using Innovation4Austria.web.AppCode;
using Innovation4Austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Verwaltung;

namespace Innovation4Austria.web.Controllers
{
    public class Innovation4AustriaMitarbeiterController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly i4aMembershipProvider membershipProvider = new i4aMembershipProvider();

        private static readonly i4aRoleProvider roleProvider = new i4aRoleProvider();

        [Authorize]
        [HttpGet]
        public ActionResult FirmenAuflistung()
        {
            log.Info("Innovation4AutriaController - FirmenAuflistung - Get");

            List<FirmenModel> FirmenUI = AutoMapper.Mapper.Map<List<FirmenModel>>(FirmenVerwaltung.LadeAlleFirmen());
            if (FirmenUI != null)
            {
                log.Error("Innovatation4AustriaController - firmenAuflistung - keine Firmen gefunden");
            }
            return View(FirmenUI);
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult FirmenAuflistung(FirmenModel model)
        {
            log.Info("Innovatation4AustriaController - FirmenAuflistung - Post");

            if (ModelState.IsValid)
            {
                if (FirmenVerwaltung.FirmaAktualisierung(model.Id, model.Bezeichnung, model.aktiv, model.Nummer, model.Ort, model.Plz, model.Strasse))
                {
                    log.Info("Innovation4AustriaMitarbeiter - FirmenAuflistung - Post - Firma aktualisiert");
                }
                else
                {
                    log.Warn("Innovation4AustriaMitarbeiter - FirmenAuflistung - Post - Firma aktualisiert");
                }
            }
            return RedirectToAction("FirmenAuflistung");
        }

        //[ValidateAntiForgeryToken]
        [Authorize]
        [HttpGet]
        public ActionResult MitarbeiterBearbeiten(int fa_id)
        {
                        
            log.Info("Innovatation4AustriaController - MitarbeiterBearbeiten - GET");

            List<BenutzerModel> alleBenutzer = AutoMapper.Mapper.Map<List<BenutzerModel>>(BenutzerVerwaltung.LadeMitarbeiterEinerFirma(fa_id));
            if (alleBenutzer == null)
            {
                log.Error("Innovatation4AustriaController - firmenAuflistung - keine Firmen gefunden");
            }
            return View(alleBenutzer);
        }

        //[ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult MitarbeiterBearbeiten(BenutzerModel model)
        {
            log.Info("Innovatation4AustriaController - MitarbeiterBearbeiten - Post");


            if (BenutzerVerwaltung.AktualisiereMitarbeiterEinerFirma(model.Id, model.Emailadresse, model.Vorname, model.Vorname, model.Aktiv))
            {
                log.Error("Innovatation4AustriaController - AktualisiereMitarbeiterEinerFirma - Speichern hat nicht geklappt");
            }
            return RedirectToAction("FirmenAuflistung");

        }

        //[ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult MitarbeiterAnlegen(BenutzerAnlegenModel model)
        {
            if (ModelState.IsValid)
            {
                log.Info("Innovatation4AustriaController - MitarbeiterAnlegen - GET");

                if (model!= null)
                {
                    if (BenutzerVerwaltung.LegeMitarbeiterAn(model.Fa_id,model.Emailadresse, model.Vorname, model.Nachname, model.NeuesPasswort, Innovation4Austria.web.AppCode.Validation.FIRMENANSPRECHPARTNER))
                    {
                        TempData[ConstStrings.SUCCESS_MESSAGE] = Validierungen.SpeichernErfolgreich;
                    }
                    else
                    {
                        TempData[ConstStrings.ERROR_MESSAGE] = Validierungen.SpeichernFehlgeschlagen;
                    }
                }
            }
            return RedirectToAction("FirmenAuflistung");
        }

        [HttpGet]
        public ActionResult FirmaAnlegen()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RaumAuflistung(FilterModel model)
        {
            return View();
        }
    }
}
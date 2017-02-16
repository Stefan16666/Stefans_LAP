using Innovation4austria.authentication;
using Innovation4Austria.authentication;
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
            if (FirmenUI!=null)
            {
                log.Error("Innovatation4AustriaController - firmenAuflistung - keine Firmen gefunden");
            }
            return View(FirmenUI);
        }
    }
}
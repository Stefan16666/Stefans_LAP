using Innovation4Austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation4Austria.web.Controllers
{
    public class BenutzerController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        // GET: Benutzer
        public ActionResult Index()
        {
            log.Info("BenutzerController - Index (Login");
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            log.Info("BenutzerController - Index (Login");
            if (ModelState.IsValid)
            {
                if (model.Emailadresse != null)
                {

                }
            }
            return View();
        }
    }
}
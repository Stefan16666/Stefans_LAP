using RotativaTestProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RotativaTestProjekt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<RotativaModel> ListModel = new List<RotativaModel>();
            for (int i = 0; i < 3; i++)
            {
                RotativaModel model = new RotativaModel()
                {
                    Bezeichnung = "Max" + i,
                    Preis = (i + 3) * 100

                };
                ListModel.Add(model);
            }
          
            return new Rotativa.ViewAsPdf(ListModel);
        }
        public ActionResult PDF()
        {

            return View();
        }



    }
}
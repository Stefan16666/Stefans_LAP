using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class ProfilMitarbeiterModel:MitarbeiterModel
    {
        public string Rolle { get; set; }

        public string FirmenName { get; set; }
    }
}
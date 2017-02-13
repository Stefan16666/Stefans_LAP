using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class ProfilAnzeigeModel
    {
        public MitarbeiterModel derMitarbeiter { get; set; }

        public PasswortAendernModel anderesPasswort { get; set; }    
    }
}
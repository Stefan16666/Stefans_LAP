using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class WerbeBannerModel
    {
        public int Raum_id { get; set; }

        public string Raumbezeichnung { get; set; }

        public string GebäudeBezeichnung { get; set; }        

        public decimal Preis { get; set; }

       
    }
}
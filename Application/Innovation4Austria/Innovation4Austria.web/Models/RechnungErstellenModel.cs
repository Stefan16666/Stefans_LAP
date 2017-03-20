using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class RechnungErstellenModel
    {
        public int Rg_Id { get; set; }

        public string Firmenname { get; set; }

        public DateTime Datum { get; set; }

        public int Raumnummer { get; set; }

        public int Dauer { get; set; }

        public decimal Preis { get; set; }

        public decimal Gesamtpreis { get; set; }
    }
}
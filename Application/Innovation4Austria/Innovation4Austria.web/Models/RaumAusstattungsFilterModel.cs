using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class RaumAusstattungsFilterModel
    {
        public int Id { get; set; }

        public string Bezeichnung { get; set; }

        public int Ausstattungs_Id { get; set; }

        public int Anzahl_Ausstattungen { get; set; }

        public bool Auswahl { get; set; }
    }
}
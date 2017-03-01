using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class RaumModel
    {
        public int Id { get; set; }

        public string Bezeichnung { get; set; }

        public decimal Preis { get; set; }
    }
}
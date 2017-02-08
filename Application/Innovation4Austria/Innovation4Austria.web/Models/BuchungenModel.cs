using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class BuchungenModel
    {
        public string Raumnummer { get; set; }

        public string RaumArt { get; set; }

        public DateTime VonDatum { get; set; }

        public DateTime BisDatum { get; set; }
    }
}
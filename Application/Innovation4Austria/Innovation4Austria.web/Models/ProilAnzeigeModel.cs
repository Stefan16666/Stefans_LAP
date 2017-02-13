using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class ProilAnzeigeModel
    {
        public string FirmenName { get; set; }

        public string Vorname { get; set; }

        public string  Nachname { get; set; }        

        [DataType(DataType.Password)]
        public string NeuesPasswort { get; set; }

        [DataType(DataType.Password)]
        public string  NeuesPasswortBestätigung { get; set; }

        public string VergebeneRolle { get; set; }

    }
}
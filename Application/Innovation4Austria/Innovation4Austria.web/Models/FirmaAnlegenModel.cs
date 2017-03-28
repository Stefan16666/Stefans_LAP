using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class FirmaAnlegenModel
    {
        [Required]
        public string Bezeichnung { get; set; }
        [Required]
        public string Strasse { get; set; }
        [Required]
        public string Nummer { get; set; }
        [Required]
        public string Plz { get; set; }
        [Required]
        public string Ort { get; set; }

        
    }
}
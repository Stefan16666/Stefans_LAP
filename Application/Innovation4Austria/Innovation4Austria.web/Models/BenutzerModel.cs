using Innovation4Austria.web.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class BenutzerModel
    {
        public int Id { get; set; }


        public string Emailadresse { get; set; }


        public string  Nachname { get; set; }


        public string Vorname { get; set; }


        public bool Aktiv { get; set; }
    }
}
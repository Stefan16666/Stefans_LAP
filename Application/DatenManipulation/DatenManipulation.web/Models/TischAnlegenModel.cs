using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DatenManipulation.web.Models
{
    public class TischAnlegenModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "min. 2 und max 50 Zeichen", MinimumLength = 2)]
        public string Bezeichnung { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int PersonenAnzahl { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int Raum_id { get; set; }

        public List<RaumModel> alleRaume { get; set; }

    }

    public class RaumModel
    {
        public int ID { get; set; }

        public string Bezeichnung { get; set; }
    }
}
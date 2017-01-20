using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DatenManipulation.web.Models
{
    public class ModelRaum
    {

        [Required(AllowEmptyStrings = false)]
        [StringLength(50,ErrorMessage = "min. 2 und max 50 Zeichen",MinimumLength =2)]       
        public string Bezeichnung { get; set; }
    }
}
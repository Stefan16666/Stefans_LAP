using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class PasswortAendernModel
    {

        [DataType(DataType.Password)]
        public string NeuesPasswort { get; set; }

        [DataType(DataType.Password)]
        public string NeuesPasswortBestätigung { get; set; }
    }
}
using Innovation4Austria.web.AppCode;
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
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*/d)(?=.*[$@$!%*?&])[A-Za-z*/d$@$!%*?&]{8,}", ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.PASSWORT_NORM)]
        public string NeuesPasswort { get; set; }

        [DataType(DataType.Password)]
        [Compare("NeuesPasswort", ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.PASSWORT_WIEDERHOLEN)]      
        public string NeuesPasswortBestätigung { get; set; }
    }
} 
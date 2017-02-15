using Innovation4Austria.web.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class PasswortVerwaltungsModel
    {

        //[StringLength(maximumLength: 16, MinimumLength = 8)]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.PASSWORT_NORM)]
        //[DataType(DataType.Password)]
        //[DisplayName("Passwort")]
        public string NeuesPasswort { get; set; }

        //[Required(AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 16, MinimumLength = 8)]   
        //[DataType(DataType.Password)]
        //[DisplayName("Passwort wiederholen")]
        //[Compare("NeuesPasswort", ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.PASSWORT_WIEDERHOLEN)]
        public string NeuesPasswortBestätigung { get; set; }
    }
} 
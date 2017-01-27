using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    /// <summary>
    /// Modell für das Einloggen
    /// </summary>
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Das ist keine gültige Emailadresse")]
        public string Emailadresse { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z!'''-'\s]{1,40}$", ErrorMessage = "Das")]
        public string Passwort { get; set; }
    }
}
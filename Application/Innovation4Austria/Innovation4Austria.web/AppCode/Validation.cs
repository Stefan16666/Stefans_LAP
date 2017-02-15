using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.AppCode
{
    public class Validation
    {
        public const string REQUIRED = "Required";
        public const string DATATYPE_MAIL = "DataTypeMail";
        public const string DATATYPE_DATE = "DataTypeDate";
        public const string REGULAR_EXPRESSION_PASSWORD = "RegexPassword";
        public const string MAXLENGTH = "MaxLength";

        public const string PASSWORT_WIEDERHOLEN = "Passwortwiederholung stimmt nicht überein";
        public const string PASSWORT_NORM = "Minimum 8 Zeichen und max 16Zeichen";
        
    }
}
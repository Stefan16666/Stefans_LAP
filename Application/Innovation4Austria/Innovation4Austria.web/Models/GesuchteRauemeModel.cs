using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class GesuchteRauemeModel
    {
        public DateTime StartDatum { get; set; }

        public DateTime EndDatum { get; set; }

        public List<RaumModel> gesuchteRaumListe { get; set; }
    }
}
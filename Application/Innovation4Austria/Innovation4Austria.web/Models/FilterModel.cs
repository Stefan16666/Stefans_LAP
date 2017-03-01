using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class FilterModel
    {
        public int Raum_Id { get; set; }

        public DateTime BeginnDatum { get; set; }

        public DateTime EndDatum { get; set; }

        public List<RaumAusstattungsModel> Ausstattung { get; set; }

        public List<RaumArtModel> Art { get; set; }

        public List<RaumModel> AlleRaeume { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class BuchungsDetailModel:RaumModel
    {
        public DateTime VonDatum { get; set; }

        public DateTime BisDatum { get; set; }

        public List<RaumAusstattungsFilterModel> RaumAusstattung { get; set; }
    }
}
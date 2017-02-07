using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class Haus
    {
        public int Id { get; set; }

        public string Strasse { get; set; }

        public int Nummer { get; set; }

        internal void CreateMap<T1, T2>()
        {
            throw new NotImplementedException();
        }
    }
   
}

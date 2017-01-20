using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Innovation4Austira.logic
{
    public class Landverwaltung
    {
        public static List<Land> LadeAlleLaender()
        {
            List<Land> liste = new List<Land>();
            Debug.WriteLine("Landverwaltung - LadeAlleLaender");
            Debug.Indent();
            using (var context = new reisebueroEntities())
            {
                 liste = context.AlleLaender.ToList();
            }
            return liste;
           
        }
    }
}

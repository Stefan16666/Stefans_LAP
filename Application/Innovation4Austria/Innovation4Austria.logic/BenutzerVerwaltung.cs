using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Innovation4Austria.logic
{
    public class BenutzerVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static bool Anmelden()
        {
            log.Info("Anmelden()");

            return true;
        }

        public static bool Abmelden()
        {
            log.Info("Abmelden()");

            return true;
        }

        public static List<string> Laden()
        {
            log.Info("Laden()");

            List<string> alleBenutzer = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                string benutzer = "Max" + i;
                alleBenutzer.Add(benutzer);
            }
            return alleBenutzer;

        }
    }
}

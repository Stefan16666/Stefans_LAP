using Innovation4Austria.logic;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verwaltung
{
    public class BenutzerVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static bool Anmelden(string emailadresse, string passwort)
        {
            log.Info("BenutzerVerwaltung - Anmelden()");
            bool gefunden = false;
            Benutzer loginbenutzer = new Benutzer();
            if (emailadresse != null)
            {
                if (passwort !=null)
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        try
                        {
                            loginbenutzer = context.AlleBenutzer.Where(x => x.Email == emailadresse).FirstOrDefault();
                            if (loginbenutzer != null)
                            {
                                if (loginbenutzer.Passwort.SequenceEqual(Tools.GenerierePasswort(passwort)))
                                {
                                    log.Info("BenutzerVerwaltung - Anmelden - Emailadresse und Passwort wurden gefunden");
                                    gefunden = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("BenutzerVerwaltung - Anmelden fehlgeschlagen ");
                          
                        }                         
                    }
                }
            }
            return gefunden;
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

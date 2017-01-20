using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verwaltung;

namespace Log4net
{
    class Program
    {
        static void Main(string[] args)
        {

            /// die Einstellungen aus demn Config-File
            /// sollen für log4net übernommen werden!
            XmlConfigurator.Configure();

            ///erzeuge einen Log-Manager
            /// Name beliebig - sinnvoll wäre natürlich ein sprechender zB. Klassen-Name
            ILog log = LogManager.GetLogger("Hallo Welt");

            /// Protokolliere einen Eintrag vom Level
            ///     Debug
            ///     Info
            ///     Warn
            ///     Error

            log.Debug("Debug");

            log.Info("Info");

            log.Warn("Warn");

            log.Error("Error");

            // Fake - Aufrufe um Protokolleierung aus diesen Methoden einzusehene

            BenutzerVerwaltung.Laden();
        }
    }
}

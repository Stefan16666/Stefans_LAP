using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using log4net;

namespace Innovation4Austria.logic
{
    public class FirmenVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<Firma> LadeAlleFirmen()
        {
            log.Info("FirmenVerwaltung -  LadeAlleFirmen");

            List<Firma> alleFirmen = null;
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    alleFirmen = context.AlleFirmen.ToList();
                    if (alleFirmen == null)
                    {
                        log.Warn("FirmenVerwaltung - LadeAlleFirmen - keine Firmen gefunden");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("FirmenVerwlatung - ladeAlleFirmen - DatenVerbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return alleFirmen;
        }
    }
}

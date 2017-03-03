using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation4Austria.logic
{
    

    public class BildVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Ausstattung LadeAusstattung(int id)
        {
            log.Info("BildVerwaltung - LadeBild");
            Ausstattung ausstattung = new Ausstattung();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    ausstattung = context.AlleAusstattungen.Where(x => x.Id == id).FirstOrDefault();
                }
                if (ausstattung == null)
                {
                    log.Warn("BildVerwaltung - LadeAusstattung - Ausstattung konnte nicht geladen werden");
                }
            }
            catch (Exception ex)
            {
                log.Error("BildVerwaltung - LadeAusstattung - DbVerbindung fehlgeschlagen");
                if (ex.InnerException!= null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return ausstattung;
        }
       
    }
}

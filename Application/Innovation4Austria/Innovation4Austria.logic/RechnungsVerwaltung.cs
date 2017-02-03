using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation4Austria.logic
{
    public class RechnungsVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static alleRechnungenAllerFirmen AlleRechnungenEinerFirma(int fa_id)
        {
            List<Rechnung> alleRechnungen = null;
            log.Info("RechnungsVerwaltung - AlleRechnungenEinerFimra");
            if (fa_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        if (context.AlleRechnungenAllerFirmen.Where(x => x.Firmen_id == fa_id).FirstOrDefault()!=null)
                        {
                            return context.AlleRechnungenAllerFirmen.Where(x => x.Firmen_id == fa_id).FirstOrDefault();
                        }

                    }
                }
                catch (Exception ex)
                {
                    log.Error("");
                }
            }
            return null;
        }
    }
}

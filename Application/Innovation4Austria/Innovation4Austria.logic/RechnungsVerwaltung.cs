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


        /// <summary>
        /// ein Rechnungsdetail einer Firma wird herausgesucht 
        /// </summary>
        /// <param name="buchungsDetail_id"></param>
        /// <returns></returns>
        public static Rechnungsdetails EinRechnungsDetailsEinesBuchungsDetails(int buchungsDetail_id)
        {
            Rechnungsdetails detail = null;
            log.Info("RechnungsVerwaltung - AlleRechnungsDetailsEinerBuchung");
            if (buchungsDetail_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {

                        {
                            detail = context.AlleRechnungsdetails.Where(x => x.Buchungsdetail_Id == buchungsDetail_id).FirstOrDefault();
                            if (detail == null)
                            {
                                log.Warn("RechnungsVerwaltung - AlleRechnungsDetailsEinerBuchung - es wurden keine Rechnungsdetails zu der Buchung gefunden");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("RechnungsVerwaltung - AlleRechnungsDetailsEinerBuchung - es konnte keine Datenbankverbindung hergestellt werden", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return detail;
        }
    }
}

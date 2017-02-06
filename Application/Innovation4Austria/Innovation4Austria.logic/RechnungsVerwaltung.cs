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

        public static Rechnungsdetails EinRechnungsDetailsEinerBuchung(int buchungsDetail_id)
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
                    log.Error("RechnungsVerwaltung - AlleRechnungsDetailsEinerBuchung - es konnte keine Datenbankverbindung hergestellt werden",ex);
                    if (ex.InnerException!= null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return detail;
        }

        //public static List<Buchungsdetails> alleRechnungsDetails()
        //{
        //    List<Rechnungsdetails> alleRechnungsDetails = null;
        //    log.Info("RechnungsVerwaltung - AlleRechnnungsDetails");
        //    try
        //    {
        //        using (var context = new Innovation4AustriaEntities())
        //        {
        //            alleRechnungsDetails = context.AlleRechnungsdetails.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("RechnungsVerwaltung - alleRechnungsDetails - es konnte keine Datenbankverbindung hergestellt werden", ex);
        //        if (ex.InnerException != null)
        //        {
        //            log.Info(ex.InnerException);
        //        }
                
        //    }
        //    return alleRechnungsDetails;
        //}
    }
}

using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation4Austria.logic
{
    public class RaumVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<Buchung> GebuchteRaeume(int fa_id)
        {
            log.Info("RaumVerwaltung - BookedRooms");
            List<Buchung> bookedRooms = new List<Buchung>();
            if (fa_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {

                        //bookedRooms = context.AlleBuchungen.Include("Buchungsdetails").Where(x => x.Firma_id == fa_id).ToList();
                        bookedRooms = context.AlleBuchungen.Where(x => x.Firma_id == fa_id).ToList();

                        if (bookedRooms == null)
                        {
                            log.Info("RaumVerWaltung - BookedRooms - There are no booked rooms found");
                        }

                    }
                }
                catch (Exception ex)
                {
                    log.Error("RaumVerwaltung - BookedRooms - Connection to database failed", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return bookedRooms;
        }

        public static List<Buchungsdetails> BuchungsDetailsVonBuchung(int buchung_id)
        {
            log.Info("RaumVerwaltung - BuchungsDetailsVonBuchung");
            List<Buchungsdetails> detailsVonBuchung = new List<Buchungsdetails>();
            if (buchung_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        detailsVonBuchung = context.AlleBuchungsdetails.Where(x => x.Buchung_id == buchung_id).ToList();
                        if (detailsVonBuchung== null)
                        {
                            log.Warn("RaumVerwaltung - BuchungsDetailsVonBuchung - keine Details zur Buchung gefunden");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("RaumVerwaltung - BuchungsDetailsVonBuchung - Datenbankverbindung fehlgeschlagen",ex);
                    if (ex.InnerException!=null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return detailsVonBuchung;

        }

    }
}

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Innovation4Austria.logic
{
    public class BuchungsVerwaltung
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

        /// <summary>
        /// reourniere das Anfangs- oder Enddatum einer Buchung auswählbar über bool "vonDatum"
        /// </summary>
        /// <param name="buchung_id"></param>
        /// <param name="vonDatum"></param>
        /// <returns></returns>
        public static DateTime datum(int buchungsDetail_id, bool vonDatum)
        {
            List<Buchungsdetails> buchungsdetailsEinerBuchung = null;
            DateTime date = new DateTime();
            Console.Write("RechnungsVerwaltung - datum");
            if (buchungsDetail_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        buchungsdetailsEinerBuchung = context.AlleBuchungsdetails.Where(x => x.Buchung_id == buchungsDetail_id).ToList();
                        if (vonDatum)
                        {
                            date = (from x in buchungsdetailsEinerBuchung orderby x.Datum select x.Datum).FirstOrDefault();
                        }
                        else
                        {
                            date = (from x in buchungsdetailsEinerBuchung orderby x.Datum descending select x.Datum).FirstOrDefault();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("RechnungsVerwaltung - Buchung - es konnte keine Datenbankverbindung hergestellt werden", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return date;
        }

        public static List<Buchungsdetails> BuchungsDetailsVonBuchung(int buchung_id)
        {
            log.Info("RaumVerwaltung - BuchungsDetailsVonBuchung");
            List<Buchungsdetails> detailVonBuchung = null;
            if (buchung_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        detailVonBuchung = context.AlleBuchungsdetails.Include("Rechnungsdetails").Where(x => x.Buchung_id == buchung_id).ToList();
                        if (detailVonBuchung == null)
                        {
                            log.Warn("RaumVerwaltung - BuchungsDetailsVonBuchung - keine Details zur Buchung gefunden");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("RaumVerwaltung - BuchungsDetailsVonBuchung - Datenbankverbindung fehlgeschlagen", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return detailVonBuchung;
        }

        
    }
}


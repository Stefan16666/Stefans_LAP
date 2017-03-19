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

        public static bool ErstelleBuchungDetails(int buchung_id, DateTime vonDatum, DateTime BisDatum, decimal preis)
        {
            bool erfolgreich = false;
            log.Info("BuchungsVerwaltung - ErstelleBuchungDetails");
            if (buchung_id >0)
            {
                if (vonDatum!= null)
                {
                    if (BisDatum !=null)                        
                    {
                        int wievieleTage = (BisDatum-vonDatum).Days;
                        wievieleTage++;
                        try
                        {
                            using (var context = new Innovation4AustriaEntities())
                            {
                                for (int i = 0; i < wievieleTage; i++)
                                {
                                    Buchungsdetails neueBuchung = new Buchungsdetails()
                                    {
                                        Buchung_id = buchung_id,
                                        Datum = vonDatum,
                                        Preis = preis
                                    };
                                    context.AlleBuchungsdetails.Add(neueBuchung);
                                    if (vonDatum<BisDatum)
                                    {
                                        vonDatum = vonDatum.AddDays(1);
                                    }
                                }
                                if (context.SaveChanges()==wievieleTage)
                                {
                                    erfolgreich = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("BuchungsVerwaltung - ErstelleBuchungDetals - fehlgeschlagen");
                            if (ex.InnerException!=null)
                            {
                                log.Info(ex.InnerException);
                            }
                        }                  

                    }
                }
            }
            return erfolgreich;
            
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
                       
                        detailVonBuchung = context.AlleBuchungsdetails.Include(x=>x.Rechnungsdetails).Where(x => x.Buchung_id == buchung_id).ToList();
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

        public static int ErstelleBuchung(int raum_id, int firma_id)
        {
            Buchung neueBuchung = new Buchung();
            neueBuchung.Raum_id = raum_id;
            neueBuchung.Firma_id = firma_id;
            log.Info("BuchungsVerwaltung - ErstelleBuchung");
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    context.AlleBuchungen.Add(neueBuchung);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return neueBuchung.Id;
        }

        
    }
}


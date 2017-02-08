using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation4Austria.logic
{
    public class BuchungsVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// reourniere das Anfangs- oder Enddatum einer Buchung auswählbar über bool "vonDatum"
        /// </summary>
        /// <param name="buchung_id"></param>
        /// <param name="vonDatum"></param>
        /// <returns></returns>
        public static DateTime datum(int buchung_id, bool vonDatum)
        {
            List<Buchungsdetails> buchungsdetailsEinerBuchung = null;
            DateTime date = new DateTime();
            Console.Write("RechnungsVerwaltung - datum");
            if (buchung_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        buchungsdetailsEinerBuchung = context.AlleBuchungsdetails.Where(x => x.Buchung_id == buchung_id).ToList();
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
    }
}


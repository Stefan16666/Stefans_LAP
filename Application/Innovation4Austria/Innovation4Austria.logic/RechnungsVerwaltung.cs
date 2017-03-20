using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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

        public static List<Rechnung> RechnungenEinerFirma(int firma_id)
        {
            log.Info("RechnugnsVerwaltung - RechnungenEinerFirma");

            List<Rechnung> rechnungen = new List<Rechnung>();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    rechnungen = context.AlleRechnungen.Where(x => x.fa_id == firma_id).ToList();
                }

            }
            catch (Exception ex)
            {
                log.Error("RechnugnsVerwaltung - RechnungenEinerFirma - DB_Verbindung fehlgeschlagen");
            }
            return rechnungen;
        }

        public static List<Rechnungsdetails> RechnungsDetailsEinerRechnung(int id)
        {
            log.Info("RechnugnsVerwaltung - RechnungsDetailsEinerRechnung");

            List<Rechnungsdetails> RgDetails = new List<Rechnungsdetails>();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    RgDetails = context.AlleRechnungsdetails.Where(x => x.Rechnung_Id == id).ToList();
                }

            }
            catch (Exception ex)
            {
                log.Error("RechnugnsVerwaltung - RechnungsDetailsEinerRechnung - DB_Verbindung fehlgeschlagen");
            }
            return RgDetails;
        }

        public static Buchungsdetails BuchungsDetailEinerRechnung(int id)
        {
            log.Info("RechnugnsVerwaltung - RechnungsDetailsEinerRechnung");

            Buchungsdetails buchungsDetail = new Buchungsdetails();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    buchungsDetail = context.AlleBuchungsdetails.Where(x => x.Id == id).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                log.Error("RechnugnsVerwaltung - RechnungsDetailsEinerRechnung - DB_Verbindung fehlgeschlagen");
            }
            return buchungsDetail;
        }
    }
}

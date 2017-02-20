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
                    alleFirmen = context.AlleFirmen.Where(x => x.aktiv).ToList();
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

        public static bool FirmaAktualisierung(int id, string bezeichnung, bool deaktivieren, string nummer, string ort, string plz, string strasse)
        {
            bool erfolgreich = false;
            log.Info("FirmenVerwaltung - FirmaAktualisierung");
            Firma aktFirma = null;

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    aktFirma = context.AlleFirmen.Where(x => x.Id == id).FirstOrDefault();
                    if (!deaktivieren)
                    {
                        aktFirma.Bezeichnung = bezeichnung;
                        aktFirma.Nummer = nummer;
                        aktFirma.Ort = ort;
                        aktFirma.Plz = plz;
                        aktFirma.Strasse = strasse;
                        erfolgreich = true;
                    }
                    else
                    {
                        aktFirma.aktiv = deaktivieren;
                        erfolgreich = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("FirmenVerwaltung - FirmaAktualisierung - DbVerbindung fehlgeschlagen");
                if (ex.InnerException!=null)
                {
                    log.Info(ex.InnerException);
                }

            }
            return erfolgreich;
        }
        
    }
}

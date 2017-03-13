﻿using log4net;
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



        public static Raum GesuchterRaum(int raum_id)
        {
            log.Info("RaumVerwaltung - gesuchterRaum");
            Raum raum  = null;
            if (raum_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        raum = context.AlleRaeume.Where(x => x.Id == raum_id).Include("Art").FirstOrDefault();
                        if (raum == null)
                        {
                            log.Warn("RaumVerwaltung - gesuchterRaum - kein Raum gefunden");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("RaumVerwaltung - gesuchterRaum - Datenbankverbindung fehlgeschlagen", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return raum;

        }



        public static List<Raum> alleRaeume()
        {
            log.Info("RaumVerwaltung - alleRaeume");

            List<Raum> mAlleRaeume = null;

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    mAlleRaeume = context.AlleRaeume.Include(x=>x.Art).ToList();
                    if (mAlleRaeume == null)
                    {
                        log.Warn("Raeume wurden nicht gefunden");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwltung - alleRaeume - Datenbankverbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }

            return mAlleRaeume;
        }

        public  static  List<Art> AlleRaumArten()
        {

            log.Info("RaumVerwaltung - alleRaumArten");

            List<Art> alleRaumArten = null;

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    alleRaumArten = context.AlleArten.ToList();
                    if (alleRaumArten == null)
                    {
                        log.Warn("RaumVerwaltung - AlleRaumArten");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwaltung - AlleRaumArten - Db-Verbindung fehlgeschlagen");
                if (ex.InnerException!= null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return alleRaumArten;
        }

        public static List<Ausstattung> AlleRaumAusstattungen()
        {
            log.Info("RaumVerwaltung - AlleRaumAusstattungen");

            List<Ausstattung> alleAusstattungen = null;

            try
            {
                using(var context = new Innovation4AustriaEntities())
                {
                    alleAusstattungen = context.AlleAusstattungen.ToList();
                    if (alleAusstattungen==null)
                    {
                        log.Warn("RaumVerwaltung - AlleRaumAusstattungen - RaumAusstattungen auslesen fehlgeschlagen");
                    }
                }
            }
            catch (Exception ex )
            {
                log.Error("RaumVerwaltung - AlleRaumAusstattungen - DB - Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }                
            }
            return alleAusstattungen;
        }

        public static List<Raum> GesuchteRaeume(DateTime startdatum, DateTime enddatum, int art_id, int[] ausstattung )
        {
            log.Info("RaumVerwaltung - GesuchteRaueme");

            List<Raum> gesuchteRaeume = new List<Raum>();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    List<Raum> alleRaeume = new List<Raum>();
                    List<Buchung> buchungen = new List<Buchung>();
                    
                    List<Buchungsdetails> buchungsdetails = context.AlleBuchungsdetails.Where(x => x.Datum >=startdatum&& x.Datum<=enddatum).ToList();
                   

                    foreach (var buchungdetail in buchungsdetails)
                    {
                        Raum derRaum = context.AlleRaeume.Include(x => x.AlleBuchungen).Where((y => y.Id == buchungdetail.Buchung_id)).Where(x=>x.Art_id==art_id).FirstOrDefault();
                        if (!alleRaeume.Contains(derRaum))
                        {
                            alleRaeume.Add(derRaum);
                        }
                    }


                    gesuchteRaeume = context.AlleRaeume.Include(x => x.Art).Include(x => x.AlleRaum_Ausstattungen).ToList();

                    List<Raum_Ausstattung> raumAusstattung = context.AlleRaum_Ausstattungen.ToList();
                    List<Raum_Ausstattung> gesuchteAusstattung = new List<Raum_Ausstattung>();

                    foreach (var raum in alleRaeume)
                    {
                        for (int i = 0; i < ausstattung.Length; i++)
                        {
                            if (!raum.AlleRaum_Ausstattungen.Select(x => x.Ausstattungs_Id).ToList().Contains(ausstattung[i]))
                            {
                                alleRaeume.Remove(raum);
                            }
                        }
                    }
                   
                    // Except - Operator
                    foreach (var raum in alleRaeume)
                    {
                        if (gesuchteRaeume.Contains(raum))
                        {
                            gesuchteRaeume.Remove(raum);
                        }
                    }

            
                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwaltung - GesuchteRaeume - DB-Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return gesuchteRaeume;
        }

        public static List<Raum> GesuchteRaeume()
        {
            log.Info("RaumVerwaltung - GesuchteRaeume");

            List<Raum> gesuchteRaeume = new List<Raum>();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    gesuchteRaeume = context.AlleRaeume.Include(x => x.Art).Include(y=>y.Bauwerk).ToList();
                    if (gesuchteRaeume==null)
                    {
                        log.Warn("RaumVerwaltung - gesuchteRaeume - keine Raeume gefunden");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwaltung - GesuchteRaeume - db-Verbindung fehlgeschlagen");
                if (ex.InnerException!=null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return gesuchteRaeume;
        }
    }
}

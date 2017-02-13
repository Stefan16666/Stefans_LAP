using Innovation4Austria.logic;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using innovation4austria.logic;

namespace Verwaltung
{
    public class BenutzerVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static bool Anmelden(string emailadresse, string passwort)
        {
            log.Info("BenutzerVerwaltung - Anmelden()");
            bool gefunden = false;
            Benutzer loginbenutzer = new Benutzer();
            if (emailadresse != null)
            {
                if (passwort != null)
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        try
                        {
                            loginbenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == emailadresse).FirstOrDefault();
                            if (loginbenutzer != null)
                            {
                                if (loginbenutzer.Passwort.SequenceEqual(Tools.GenerierePasswort(passwort)))
                                {
                                    log.Info("BenutzerVerwaltung - Anmelden - Emailadresse und Passwort wurden gefunden");
                                    gefunden = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("BenutzerVerwaltung - Anmelden fehlgeschlagen ");
                            log.Info(ex.Message);
                        }
                    }
                }
            }
            return gefunden;
        }

        public static bool Abmelden()
        {
            log.Info("Abmelden()");

            return true;
        }

        public static BenutzerAdministrator.Passwortwechselergebnis Wechselpasswort(string emailadresse, string neuePassword, string altesPasswort)
        {

            BenutzerAdministrator.Passwortwechselergebnis result = BenutzerAdministrator.Passwortwechselergebnis.UsernameInvalid;

            log.Info("ChangePassword(username, oldPassword, newPassword)");

            if (string.IsNullOrEmpty(emailadresse))
                throw new ArgumentNullException(nameof(emailadresse));
            else if (string.IsNullOrEmpty(neuePassword))
                throw new ArgumentNullException(nameof(neuePassword));
            else if (string.IsNullOrEmpty(altesPasswort))
                throw new ArgumentNullException(nameof(altesPasswort));
            else
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    try
                    {
                        Benutzer aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == emailadresse).FirstOrDefault();

                        if (aktBenutzer == null)
                        {
                            result = BenutzerAdministrator.Passwortwechselergebnis.UsernameInvalid;
                        }
                        else if (!aktBenutzer.Aktiv == true)
                        {
                            result = BenutzerAdministrator.Passwortwechselergebnis.UserInactive;
                        }
                        else if (!aktBenutzer.Passwort.SequenceEqual(Tools.GenerierePasswort(altesPasswort)))
                        {
                            result = BenutzerAdministrator.Passwortwechselergebnis.PasswortInvalid;
                        }
                        else
                        {
                            log4net.LogicalThreadContext.Properties["idUser"] = aktBenutzer.Id;

                            aktBenutzer.Passwort = Tools.GenerierePasswort(neuePassword);
                            context.SaveChanges();

                            result = BenutzerAdministrator.Passwortwechselergebnis.Success;
                            log.Info("Passwort aufgrund altem Passwort erfolgreich geändert!");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Fehler bei BenutzerPasswortÄndern", ex);
                        if (ex.InnerException != null)
                            log.Error("Fehler bei BenutzerPasswortÄndern (inner)", ex.InnerException);
                        throw;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Sending back the company where a person works
        /// </summary>
        /// <param name="emailadress"></param>
        public static Firma LadeFirmaVonBenutzer(string emailadress)
        {
            log.Info("BenutzerVerwaltung - LoadCompanyOfAUser");
            Firma gesuchteFirma = new Firma();
            if (!string.IsNullOrEmpty(emailadress))
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        Benutzer user = new Benutzer();
                        user = context.AlleBenutzer.Where(x => x.Emailadresse == emailadress).FirstOrDefault();
                        if (user != null)
                        {
                            gesuchteFirma = context.AlleFirmen.Where(x => x.Id == user.Firma_id).FirstOrDefault();
                            if (gesuchteFirma != null)
                            {
                                
                                log.Error("BenutzerVerwaltung - LoadCompanyOfAUser - the company with the fa_id was not found");
                            }
                        }
                        else
                        {
                            log.Error("BenutzerVerwaltung - LoadCompanyOfAUser - the user with the emailadress was not found");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Connection to database failed");
                    log.Info(ex.Message, ex.InnerException);
                }
            }
            return gesuchteFirma;
        }

        /// <summary>
        /// search where does the user with the emailadress works and send back the hole stuff of the company
        /// </summary>
        /// <param name="emailadresse"></param>
        /// <returns></returns>
        public static List<Benutzer> LoadStuffOfACompany(int fa_id)
        {
            List<Benutzer> AllStuffofCompany = new List<Benutzer>();
            log.Info("BenutzerVerwaltung - LoadStuffOfACompany");
            if (fa_id > 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {

                        AllStuffofCompany = context.AlleBenutzer.Where(x => x.Firma_id == fa_id).ToList();
                        if (AllStuffofCompany.Count < 1)
                        {
                            log.Error("BenutzerVerwaltung - LoadStuffOfACompany - There is a mistake by finding stuff from a company");
                        }
                        else
                        {
                            log.Info("BenutzerVerwaltung - LoadStuffOfACompany - Loading stuff was successful");
                            return AllStuffofCompany;
                        }

                    }
                }
                catch (Exception ex)
                {
                    log.Error("BenutzerVerwaltung - LoadStuffOfACompany - The connection to database was not possible");
                    log.Info(ex.Message, ex.InnerException);
                }
            }
            return null;
        }

        public static List<Firma> LadeAlleFirmen()
        {
            List<Firma> alleFirmen = null;
            log.Info("BenutzerVerwaltung - LadeAlleFirmen");
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    alleFirmen = context.AlleFirmen.ToList();
                    if (alleFirmen == null)
                    {
                        log.Error("BenutzerVerwaltung - ");
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("BenutzerVerwaltung - LadeAllefirmen - konnte nicht geladen werden");
                if (ex.InnerException!=null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return alleFirmen;
        }

        public static bool DeaktiviereBenutzer(string emailadresse)
        {
            bool erfolgreich = false;
            Benutzer aktBenutzer = null;
            log.Info("DeaktiviereBenuter(emailadresse)");
            if (string.IsNullOrEmpty(emailadresse))
            {
                return erfolgreich;
            }
            else
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == emailadresse).FirstOrDefault();
                        aktBenutzer.Aktiv = false;
                        int anzahlAenderung = context.SaveChanges();
                        if (anzahlAenderung > 0)
                        {
                            erfolgreich = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                    log.Error("BenutzerVerwaltung-DeaktiviereBenutzer missed");
                    log.Info(ex.Message, ex.InnerException);
                }
            }
            return true;
        }
        public static bool AktiviereBenutzer(string emailadresse)
        {
            Benutzer aktBenutzer = null;
            bool erfolgreich = false;
            log.Info("BenutzerVerwaltung - AktiviereBenutzer");
            if (!string.IsNullOrEmpty(emailadresse))
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == emailadresse).FirstOrDefault();
                        aktBenutzer.Aktiv = true;
                        int anzahlAenderung = context.SaveChanges();
                        if (anzahlAenderung > 0)
                        {
                            erfolgreich = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("BenutzerVerwaltung - AktiviereBenutzer fahlgeschlagen");
                    log.Info(ex.Message, ex.InnerException);

                }
            }
            return erfolgreich;
        }
    }
}


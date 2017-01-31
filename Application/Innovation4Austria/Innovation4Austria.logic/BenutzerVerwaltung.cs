using Innovation4Austria.logic;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static Passwortwechselergebnis Wechselpasswort(string emailadresse, string neuePassword, string altesPasswort)
        {

            Passwortwechselergebnis result = Passwortwechselergebnis.UsernameInvalid;

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
                            result = Passwortwechselergebnis.UsernameInvalid;
                        }
                        else if (!aktBenutzer.Aktiv == true)
                        {
                            result = Passwortwechselergebnis.UserInactive;
                        }
                        else if (!aktBenutzer.Passwort.SequenceEqual(Tools.GenerierePasswort(altesPasswort)))
                        {
                            result = Passwortwechselergebnis.PasswortInvalid;
                        }
                        else
                        {
                            log4net.LogicalThreadContext.Properties["idUser"] = aktBenutzer.Id;

                            aktBenutzer.Passwort = Tools.GenerierePasswort(neuePassword);
                            context.SaveChanges();

                            result = Passwortwechselergebnis.Success;
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
                        if (anzahlAenderung>0)
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


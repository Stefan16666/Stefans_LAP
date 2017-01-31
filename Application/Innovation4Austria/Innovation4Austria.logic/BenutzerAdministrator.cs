using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Innovation4Austria.logic;

namespace innovation4austria.logic
{
    public class BenutzerAdministrator
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<Benutzer> GetRoleUsers(string rollenName)
        {
            log.Info("GetRoleUsers(rolenName)");

            if (string.IsNullOrEmpty(rollenName))
            {
                throw new ArgumentNullException(nameof(rollenName));
            }
            else
            {
                List<Benutzer> benutzerRollen = null;

                using (var context = new Innovation4AustriaEntities())
                {
                    try
                    {
                        Rolle aktRolle = context.AlleRollen.Where(x => x.Bezeichnung == rollenName).FirstOrDefault();
                        if (aktRolle != null)
                        {
                            benutzerRollen = aktRolle.AlleBenutzer.Where(x => x.Aktiv== true).ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in GetRoleUsers", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in GetRoleUsers (inner)", ex.InnerException);
                        throw;
                    }
                }

                return benutzerRollen;
            }
        }

        public static List<Role> GetRoles()
        {
            log.Info("GetRoles()");
            List<Role> rollen = null;

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    rollen = context.AllRoles.Where(x => x.Active).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetRoles", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetRoles (inner)", ex.InnerException);
                    throw;
                }
            }

            return rollen;
        }

        public static Role GetUserRole(string username)
        {
            log.Info("GetUserRoles(username)");

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            else
            {
                Role userRole = null;

                using (var context = new innovation4austriaEntities())
                {
                    try
                    {
                        User aktBenutzer = context.AllUsers.Where(x => x.Username == username).FirstOrDefault();
                        if (aktBenutzer != null)
                        {
                            userRole = aktBenutzer.Role;
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in GetUserRole", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in GetUserRole (inner)", ex.InnerException);
                        throw;
                    }
                }

                return userRole;
            }
        }
    }
}

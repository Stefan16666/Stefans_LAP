using Innovation4Austria.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Innovation4Austria.authentification
{
    public class i4aRoleProvider : RoleProvider
    {

        string applicationName = "Innovation4Austria database";
        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    applicationName = value;
                }
            }
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  searching for all names of a role
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="usernameToMatch"></param>
        /// <returns></returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// return all roles
        /// </summary>
        /// <returns></returns>
        public override string[] GetAllRoles()
        {
            List<Rolle> allRolles = RoleAdministration.GetRoles();
            if (allRolles != null)
            {
                return allRolles.Select(x => x.Bezeichnung).ToArray();
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>
        ///  get all roles which the user has
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            Rolle userRoles = RoleAdministration.GetUserRole(username);
            if (userRoles != null)
            {
                return new string[] { userRoles.Bezeichnung };
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// returns all user of a role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override string[] GetUsersInRole(string roleName)
        {
            List<Benutzer> userOfARole = RoleAdministration.GetRoleUser(roleName)
            return null;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}

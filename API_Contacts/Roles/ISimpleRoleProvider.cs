using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contacts.Roles
{
    public interface ISimpleRoleProvider
    {
        ///<summary>
        ///Loads and returns the roles names for a given user name
        /// </summary>
        /// <param name="userName"> the login name of the user for which to return the roles</param>
        ///<returns>
        /// collection of <see cref="string"/> that describes the roles assigned to the user;
        /// An empty collection of no roles are assigned to the user
        ///</returns>
        ///
        Task<ICollection<string>> GetUserRolesAsync(string userName);
    }
}

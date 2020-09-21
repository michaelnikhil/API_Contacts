using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contacts.Roles
{
    public class CustomWindowsAuthenticationProvider : ISimpleRoleProvider
    {
        public const string ADMIN = "Admin";
        public const string BASIC_USER = "BasicUser";

        public Task<ICollection<string>> GetUserRolesAsync(string userName)
        {
            ICollection<string> result = new string[0];
            ///<summary>
            ///One basic user and one admin hard coded
            /// </summary>
            if (!string.IsNullOrEmpty(userName))
            {
                if (userName.EndsWith("jules", StringComparison.OrdinalIgnoreCase))
                {
                    result = new[] { BASIC_USER };
                }
                else if (userName.EndsWith("michael", StringComparison.OrdinalIgnoreCase))
                {
                    result = new[] { ADMIN };
                }
            }
            return Task.FromResult(result);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contacts.Roles
{
    /// <summary>
    /// Provides the extension methods to enable and register the simple role authentication on an Asp.Net Core web site.
    /// </summary>
    public class SimpleRoleAuthorizationServiceCollectionExtensions
    {


        /// <summary>
        /// Activates simple role authorization for Windows authentication for the ASP.Net Core web site.
        /// </summary>
        /// <typeparam name="TRoleProvider">The <see cref="Type"/> of the <see cref="ISimpleRoleProvider"/> implementation that will provide user roles.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> onto which to register the services.</param>
        //public static void AddSimpleRoleAuthorization<TRoleProvider>(this IServiceCollection services)
        //    where TRoleProvider : class, ISimpleRoleProvider
        //{
        //    services.AddSingleton<ISimpleRoleProvider, TRoleProvider>();
        //    services.AddSingleton<IClaimsTransformation, SimpleRoleAuthorizationTransform>();
        //}



    }
}

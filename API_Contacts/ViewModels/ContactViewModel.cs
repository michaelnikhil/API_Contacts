using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contacts.ViewModels
{
    /// <summary>
    ///   this class is used to display the contacts and their list of skills
    /// </summary>
    /// <remarks> Only the skill names are displayed. To corresponding skill level can be displayed in the other tables </remarks>
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<string> Skills { get; set; }
    }
}

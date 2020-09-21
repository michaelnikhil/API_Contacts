using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contacts.ViewModels
{
    /// <summary>
    ///   this class is used to display the association between skills and contacts
    /// </summary>
    /// <remarks> The full name and skill name level are constructed at the level of the controller </remarks>
    public class ContactSkillViewModel
    {
        public int IdContact { get; set; }
        public int IdSkill { get; set; }
        public string FullName { get; set; }
        public string SkillNameLevel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contacts.ViewModels
{
    public class ContactSkillViewModel
    {
        public int IdContact { get; set; }
        public int IdSkill { get; set; }
        public string FullName { get; set; }
        public string SkillNameLevel { get; set; }
    }
}

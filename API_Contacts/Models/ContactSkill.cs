using System;
using System.Collections.Generic;

namespace API_Contacts.Models
{
    public partial class ContactSkill
    {
        public int IdSkill { get; set; }
        public int IdContact { get; set; }

        public virtual Contact IdContactNavigation { get; set; }
        public virtual Skill IdSkillNavigation { get; set; }
    }
}

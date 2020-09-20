using System;
using System.Collections.Generic;

namespace API_Contacts.Models
{
    public partial class Skill
    {
        public Skill()
        {
            ContactSkill = new HashSet<ContactSkill>();
        }

        public int Id { get; set; }
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }

        public virtual ICollection<ContactSkill> ContactSkill { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace API_Contacts.Models
{
    /// <summary>
    ///   this class describes the attribues of the skill
    /// </summary>
    /// <remarks> there is a link to contactskill table </remarks>
    

    public partial class Skill
    {
        public Skill()
        {
            ContactSkill = new HashSet<ContactSkill>();
        }

        public int Id { get; set; }
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }

        //relationship to skill table via an association table (many to many)
        public virtual ICollection<ContactSkill> ContactSkill { get; set; }
    }
}

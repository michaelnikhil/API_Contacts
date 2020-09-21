using System;
using System.Collections.Generic;

namespace API_Contacts.Models
{
    //this class makes the link between contact and skills (many to many relationship)
    //composite primary key : IdSkill, IdContact
    public partial class ContactSkill
    {
        public int IdSkill { get; set; }
        public int IdContact { get; set; }

        public virtual Contact IdContactNavigation { get; set; }
        public virtual Skill IdSkillNavigation { get; set; }
    }
}

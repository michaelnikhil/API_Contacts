using System;
using System.Collections.Generic;

namespace API_Contacts
{
    public partial class Contact
    {
        public Contact()
        {
            ContactSkill = new HashSet<ContactSkill>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<ContactSkill> ContactSkill { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_Contacts.Models
{
    public partial class Contact
    {
        public Contact()
        {
            ContactSkill = new HashSet<ContactSkill>();
        }

        private string _Email; //for validation of the email
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email
        {
            get { return _Email; }
            set
            {
                bool isValid = new EmailAddressAttribute().IsValid(value);
                if (isValid)
                {
                    _Email = value;
                }
                else
                {
                    //TODO improve exception handling
                    _Email = "invalid_email";
                }
            }
        }
        public string PhoneNumber { get; set; }

        public virtual ICollection<ContactSkill> ContactSkill { get; set; }
    }
}

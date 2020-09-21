using Microsoft.Net.Http.Headers;
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
        private string _PhoneNumber; //for validation of the phone number

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        //email validation setter
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
                    throw new ArgumentException("Enter a valid email");
                }
            }
        }
        //phone number validation setter
        public string PhoneNumber {
            get { return _PhoneNumber; }
            set 
            {
                int number;
                if (int.TryParse(value, out number))
                {
                    _PhoneNumber = value;
                }
                else
                {
                    throw new ArgumentException("Enter a valid phone number");
                }
            }
                
        }

        //relationship to skill table via an association table (many to many)
        public virtual ICollection<ContactSkill> ContactSkill { get; set; }
    }
}

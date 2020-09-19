using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contacts.ViewModels
{
    public class ContactViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<string> Skills { get; set; }
    }
}

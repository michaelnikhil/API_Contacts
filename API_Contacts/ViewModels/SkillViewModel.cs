using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contacts.ViewModels
{    
    /// <summary>
     ///   this class is used to display the skills and their list of contacts (FullName)
     /// </summary>
  
    public class SkillViewModel
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }

        public List<string> FullNames { get; set; }
    }
}

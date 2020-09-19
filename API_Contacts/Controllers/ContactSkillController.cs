using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Contacts.DataAccess;
using API_Contacts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Contacts.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class ContactSkillController : ControllerBase
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly IRepository<ContactSkill> _contactskillRepository;
        private readonly IRepository<Skill> _skillRepository;

        public ContactSkillController(
            IRepository<Contact> contactRepository,
            IRepository<ContactSkill> contactskillRepository,
            IRepository<Skill> skillRepository
            )
        {
            _contactRepository = contactRepository;
            _contactskillRepository = contactskillRepository;
            _skillRepository = skillRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var contactskills = _contactskillRepository.GetAll().Select(c => new ContactSkillViewModel { 
                FullName = String.Concat(c.IdContactNavigation.FirstName, " ", c.IdContactNavigation.LastName),
                SkillNameLevel = String.Concat(c.IdSkillNavigation.SkillName, " ", c.IdSkillNavigation.SkillLevel)
            }).ToList();

            return Ok(contactskills);

        }

    }
}

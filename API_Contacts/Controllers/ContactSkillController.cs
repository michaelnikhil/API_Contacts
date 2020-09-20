using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Contacts.DataAccess;
using API_Contacts.Models;
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

        /// <summary>
        /// Gets the list of contact - skill associations
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {

            var contactskills = _contactskillRepository.GetAll().Select(c => new ContactSkillViewModel { 
                IdContact = c.IdContactNavigation.Id,
                IdSkill = c.IdSkillNavigation.Id,
                FullName = String.Concat(c.IdContactNavigation.FirstName, " ", c.IdContactNavigation.LastName),
                SkillNameLevel = String.Concat(c.IdSkillNavigation.SkillName, " ", c.IdSkillNavigation.SkillLevel)
            }).ToList();

            return Ok(contactskills);

        }
        /// <summary>
        /// Gets a contact - skill association by id
        /// </summary>
        //GET SkillContact/5
        [HttpGet("{idcontact}/{idskill}", Name = "GetSkillContact")]
        public IActionResult Get(int idcontact, int idskill)
        {
            var contactskillQuery = _contactskillRepository.GetAll()
            .Where(c => (c.IdContact == idcontact) && (c.IdSkill == idskill))
            .FirstOrDefault();

            if (contactskillQuery == null)
            {
                return BadRequest();
            }

            ContactSkillViewModel contactskill = new ContactSkillViewModel
            {
                IdContact = contactskillQuery.IdContact,
                IdSkill = contactskillQuery.IdSkill,
                FullName = String.Concat(contactskillQuery.IdContactNavigation.FirstName, " ", contactskillQuery.IdContactNavigation.LastName),
                SkillNameLevel = String.Concat(contactskillQuery.IdSkillNavigation.SkillName, " ", contactskillQuery.IdSkillNavigation.SkillLevel)
            };

            return Ok(contactskill);
        }


        /// <summary>
        /// Create contact - skill association
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// 
        ///     {
        ///         "IdContact" : 1,
        ///         "IdSkill" : 5
        ///     }
        /// </remarks>
        //post Contact
        [HttpPost]
        public IActionResult Post([FromBody] ContactSkill value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var createdContactSkill = _contactskillRepository.Add(value);

            return CreatedAtAction("Get", new { id = createdContactSkill.IdContact, createdContactSkill });

        }


        /// <summary>
        /// Delete a contact - skill association
        /// </summary>
        //delete ContactSkill/5
        [HttpDelete("{idcontact}/{idskill}")]
        public IActionResult Delete(int idcontact, int idskill)
        {
            var contactskill = _contactskillRepository.GetAll()
                .Where(c => (c.IdContact == idcontact) && (c.IdSkill == idskill))
                .FirstOrDefault();
            if (contactskill == null)
            {
                return NotFound();
            }

            _contactskillRepository.Delete(contactskill);

            return NoContent();
        }


    }
}

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
    public class ContactController : ControllerBase
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly IRepository<ContactSkill> _contactskillRepository;
        private readonly IRepository<Skill> _skillRepository;

        public ContactController(
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
            var list_contacts = new List<ContactViewModel> { };

            foreach (Contact c in _contactRepository.GetAll())
            {

                var querySkills = (from skills in _skillRepository.GetAll()
                            join contactskills in _contactskillRepository.GetAll()
                            on  skills.Id equals contactskills.IdSkill
                            where contactskills.IdContact == c.Id
                            select skills.SkillName).ToList();              

                list_contacts.Add(new ContactViewModel { 
                    Id = c.Id,
                    FirstName = c.FirstName, 
                    LastName = c.LastName, 
                    Email = c.Email,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,               
                    Skills= querySkills
                });

            }           
            return Ok(list_contacts);
        }

        //GET Contact/5
        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult Get(int id)
        {
            var contactRepo = _contactRepository.GetById(id);
            if (contactRepo == null)
            {
                return NotFound();
            }

            var querySkills = (from skills in _skillRepository.GetAll()
                         join contactskills in _contactskillRepository.GetAll()
                         on skills.Id equals contactskills.IdSkill
                         where contactskills.IdContact == contactRepo.Id
                         select skills.SkillName).ToList();

            var contactDisplay = new ContactViewModel
            {
                Id = contactRepo.Id,
                FirstName = contactRepo.FirstName,
                LastName = contactRepo.LastName,
                Email = contactRepo.Email,
                Address = contactRepo.Address,
                PhoneNumber = contactRepo.PhoneNumber,
                Skills = querySkills
            };
                        
            return Ok(contactDisplay);
        }

        //post Contact
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            if (value == null || (value.FirstName == null || value.LastName ==null || value.Email == null))
            {
                return NotFound("Enter at least : first name, last name, email");
            }
            if (value.Email == "invalid_email")
            {
                return NotFound("Enter a valid email");
            }
            
            var createdContact = _contactRepository.Add(value);

            return CreatedAtAction("Get", new { id = createdContact.Id, createdContact });

        }

        //put Contact/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Contact value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            value.Id = id;
            _contactRepository.Update(value);
            return NoContent();
        }

        //delete Contact/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contact = _contactRepository.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }

            _contactRepository.Delete(contact);

            return NoContent();
        }
    }
}

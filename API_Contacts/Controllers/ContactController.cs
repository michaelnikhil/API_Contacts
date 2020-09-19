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
                c.ContactSkill = _contactskillRepository.GetAll().Where(d => d.IdContact == c.Id).ToList();
                //var list_skills = new List<int> { };


                var query = (from skills in _skillRepository.GetAll()
                            join contactskills in _contactskillRepository.GetAll()
                            on  skills.Id equals contactskills.IdSkill
                            where contactskills.IdContact == c.Id
                            select skills.SkillName).ToList();


               

                list_contacts.Add(new ContactViewModel { FirstName = c.FirstName, LastName = c.LastName, Skills= (List<string>)query });

            }
            
            return Ok(list_contacts);
        }

        //GET Contact/5
        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult Get(int id)
        {
            var contact = _contactRepository.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        //post Contact
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var createdContact = _contactRepository.Add(value);

            return CreatedAtAction("Get", new { id = createdContact.Id, createdContact });

        }
        //TODO fix the put method
        //put Contact/5
        [HttpPost("{id}")]
        public IActionResult Put(int id, [FromBody] Contact value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var note = _contactRepository.GetById(id);

            if (note == null)
            {
                return NotFound();
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

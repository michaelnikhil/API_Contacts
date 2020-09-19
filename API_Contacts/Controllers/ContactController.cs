using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Contacts.DataAccess;
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

        public ContactController(IRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return _contactRepository.GetAll();
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

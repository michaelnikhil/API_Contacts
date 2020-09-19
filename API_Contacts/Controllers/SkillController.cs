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
    public class SkillController : ControllerBase
    {
        private readonly IRepository<Skill> _skillRepository;

        public SkillController(IRepository<Skill> skillRepository)
        {
            _skillRepository = skillRepository;
        }

        [HttpGet]
        public IEnumerable<Skill> Get()
        {
            return _skillRepository.GetAll();
        }

        //GET Skill/5
        [HttpGet("{id}", Name = "GetSkill")]
        public IActionResult Get(int id)
        {
            var skill = _skillRepository.GetById(id);
            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        //post Contact
        [HttpPost]
        public IActionResult Post([FromBody] Skill value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var createdSkill = _skillRepository.Add(value);

            return CreatedAtAction("Get", new { id = createdSkill.Id, createdSkill });

        }
        //TODO fix the put method
        //put Skill/5
        [HttpPost("{id}")]
        public IActionResult Put(int id, [FromBody] Skill value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var note = _skillRepository.GetById(id);

            if (note == null)
            {
                return NotFound();
            }

            value.Id = id;
            _skillRepository.Update(value);
            return NoContent();
        }

        //delete Skill/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var skill = _skillRepository.GetById(id);
            if (skill == null)
            {
                return NotFound();
            }

            _skillRepository.Delete(skill);

            return NoContent();
        }
    }
}

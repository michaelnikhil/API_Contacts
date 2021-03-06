﻿using System;
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
    public class SkillController : ControllerBase
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly IRepository<ContactSkill> _contactskillRepository;
        private readonly IRepository<Skill> _skillRepository;

        public SkillController(
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
        /// Get the list of skills 
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var list_skills = new List<SkillViewModel> { };

            //query the contacts for each skill to display in the view
            foreach (Skill s in _skillRepository.GetAll())
            {

                var queryContacts = (from contacts in _contactRepository.GetAll()
                                   join contactskills in _contactskillRepository.GetAll()
                                   on contacts.Id equals contactskills.IdContact
                                   where contactskills.IdSkill == s.Id
                                   select String.Concat(contacts.FirstName," ", contacts.LastName)).ToList();

                list_skills.Add(new SkillViewModel
                {
                    Id = s.Id,
                    SkillName=s.SkillName,
                    SkillLevel = s.SkillLevel,
                    FullNames = queryContacts
                });

            }
            return Ok(list_skills);
        }
        /// <summary>
        /// Get skill by id
        /// </summary>
        //GET Skill/5
        [HttpGet("{id}", Name = "GetSkill")]
        public IActionResult Get(int id)
        {

            var skillRepo = _skillRepository.GetById(id);
            if (skillRepo == null)
            {
                return NotFound();
            }

            //query the contacts for this skill to display in the view
            var queryContacts = (from contacts in _contactRepository.GetAll()
                                 join contactskills in _contactskillRepository.GetAll()
                                 on contacts.Id equals contactskills.IdContact
                                 where contactskills.IdSkill == skillRepo.Id
                                 select String.Concat(contacts.FirstName, " ", contacts.LastName)).ToList();

            var skillDisplay = new SkillViewModel
            {
                Id = skillRepo.Id,
                SkillName = skillRepo.SkillName,
                SkillLevel = skillRepo.SkillLevel,
                FullNames = queryContacts
            };

            return Ok(skillDisplay);

        }


        /// <summary>
        /// Create a skill
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// 
        ///     {
        ///         "SkillName" : "Humour",
        ///         "SkillLevel" : "Dementiel"
        ///     }
        /// </remarks>  
        //post Contact
        [HttpPost]
        //[SwaggerRequestExample(typeof(SkillReducedModel),typeof(SkillRequestModelExample))]
        public IActionResult Post([FromBody] Skill value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var createdSkill = _skillRepository.Add(value);

            return CreatedAtAction("Get", new { id = createdSkill.Id, createdSkill });

        }

        /// <summary>
        /// Update a skill
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// 
        ///     {
        ///         "SkillName" : "Humour",
        ///         "SkillLevel" : "Dementiel"
        ///     }
        /// </remarks>
        //put Skill/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Skill value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            value.Id = id;
            _skillRepository.Update(value);
            return NoContent();
        }

        /// <summary>
        /// Delete a skill
        /// </summary>
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

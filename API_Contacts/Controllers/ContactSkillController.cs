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
    public class ContactSkillController : ControllerBase
    {
        private readonly IRepository<ContactSkill> _contactskillRepository;

        public ContactSkillController(IRepository<ContactSkill> contactskillRepository)
        {
            _contactskillRepository = contactskillRepository;
        }

        [HttpGet]
        public IEnumerable<ContactSkill> Get()
        {
            return _contactskillRepository.GetAll();
        }

    }
}

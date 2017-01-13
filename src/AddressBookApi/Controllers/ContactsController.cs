using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using AddressBookApi.Dtos;
using AutoMapper;
using Model.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AddressBookApi.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepository;
        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet("{searchString?}")]
        public IEnumerable<ContactDto> GetEmployees(string searchString = "")
        {
            var contacts = _contactRepository.Get(searchString);
            var contactDtos = Mapper.Map<IEnumerable<Contact>, IEnumerable<ContactDto>>(contacts);
            return contactDtos;
        }

        [HttpGet("tags/{searchString?}")]
        public IEnumerable<TagDto> Get(string searchString = "")
        {
            var tags = _contactRepository.GetTags(searchString);
            var tagDtos = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagDto>>(tags);
            return tagDtos;
        }

        [HttpGet("contactsbyid/{id}")]
        public IEnumerable<ContactDto> GetEmployeesById(int id)
        {
            var contacts = _contactRepository.GetById(id);
            var contactDtos = Mapper.Map<IEnumerable<Contact>, IEnumerable<ContactDto>>(contacts);
            return contactDtos;
        }

        [HttpGet("contactsbytagid/{id}")]
        public IEnumerable<ContactDto> GetEmployeesByTagId(int id)
        {
            var contacts = _contactRepository.GetByTagId(id);
            var contactDtos = Mapper.Map<IEnumerable<Contact>, IEnumerable<ContactDto>>(contacts);
            return contactDtos;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Tag tag)
        {
            if (tag == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            _contactRepository.Add(tag);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Tag tag)
        {
            if (tag == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var exists = _contactRepository.Any(tag.Id);
            if (!exists)
            {
                return NotFound();
            }
            _contactRepository.Update(tag);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var exists = _contactRepository.Any(id);
            if (!exists)
            {
                return NotFound();
            }
            _contactRepository.Delete(id);
            return NoContent();
        }
    }
}

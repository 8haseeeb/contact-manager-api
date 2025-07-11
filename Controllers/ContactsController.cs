using Microsoft.AspNetCore.Mvc;
using ContactManagerApi.Models;
using ContactManagerApi.Services;

namespace ContactManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _service;
        public ContactsController(ContactService service) => _service = service;

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contact = _service.GetById(id);
            return contact == null ? NotFound() : Ok(contact);
        }

        [HttpPost]
        public IActionResult Add(Contact contact)
        {
            var created = _service.Add(contact);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Contact contact)
        {
            if (!_service.Update(id, contact)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.Delete(id)) return NotFound();
            return NoContent();
        }
    }
} 
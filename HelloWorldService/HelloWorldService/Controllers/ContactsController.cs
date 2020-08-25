using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using HelloWorldService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorldService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        public static List<Contact> contacts = new List<Contact>();
        public static int currentId = 101;

        // GET: api/<ContactsController>
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return contacts;
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var contact = contacts.FirstOrDefault(t => t.Id == id);

            if (contact == null )
            {
                return NotFound();
            }

            return new OkObjectResult(contact);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }
            value.Id = currentId++;
            value.DateAdded = DateTime.Now;
            contacts.Add(value);

            //var result = new { Id = value.Id, Candy = true };

            //result = value in reality


            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);  //gets the location of the newly created item
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Contact value)
        {
            var contact = contacts.FirstOrDefault(t => t.Id == id);

            if (contact == null)
            {
                return NotFound();
            }


           // contact = value; //method is not safe because you dont want to touch/change other values

            //safe way
            contact.Id = id;
            contact.Name = value.Name;
            contact.Phones = value.Phones;

            return Ok(contact);
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contactsRemoved = contacts.RemoveAll(t => t.Id == id);

            if (contactsRemoved == 0)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

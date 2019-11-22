using CustomModelBinder.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CustomModelBinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET: api/Person
        [HttpGet]
        public ActionResult<Person> Get()
        {
            return new Person();
        }

        // GET: api/Person
        [HttpGet("many")]
        public ActionResult<IEnumerable<Person>> Get(int count = 100)
        {
            return Enumerable.Range(0, count).Select(index => new Person()).ToList();
        }

        // GET: api/Person/GUID
        [HttpGet("{Id}")]
        public ActionResult<Person> Get([ModelBinder(Name = "id")]Person person)
        {
            return person;
        }
    }
}

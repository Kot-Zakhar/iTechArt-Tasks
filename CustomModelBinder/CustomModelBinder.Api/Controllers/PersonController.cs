using CustomModelBinder.Api.Models;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/Person/GUID
        [HttpGet("{Id}")]
        public ActionResult<Person> Get([ModelBinder(Name = "id")]Person person)
        {
            return person;
        }
    }
}

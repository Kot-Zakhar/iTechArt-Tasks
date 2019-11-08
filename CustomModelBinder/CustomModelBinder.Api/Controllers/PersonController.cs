using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomModelBinder.Api.Models;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Person/5
        [HttpGet("{base64Id}", Name = "Get")]
        public ActionResult<Person> Get(string base64Id)
        {
            return new Person(Guid.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(base64Id))));
        }
    }
}

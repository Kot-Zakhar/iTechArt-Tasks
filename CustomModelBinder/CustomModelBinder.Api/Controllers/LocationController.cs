using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomModelBinder.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomModelBinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        // GET: api/location
        [HttpGet]
        public ActionResult<Point> Get([FromQuery] List<int> coord)
        {
            if (coord.Count != 3)
                return NotFound();
            return new Point()
            {
                x = coord[0],
                y = coord[1],
                z = coord[2]
            };
        }


    }
}
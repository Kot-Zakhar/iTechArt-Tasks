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
        public ActionResult<Point> Get([FromQuery] string coord)
        {
            int[] coordinates = coord.Split(",").Select(value => Convert.ToInt32(value)).ToArray();
            if (coordinates.Length != 3)
                return NotFound();
            return new Point()
            {
                x = coordinates[0],
                y = coordinates[1],
                z = coordinates[2]
            };
        }


    }
}
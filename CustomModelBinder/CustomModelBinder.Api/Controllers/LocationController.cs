using CustomModelBinder.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomModelBinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        // GET: api/location
        [HttpGet]
        public ActionResult<Point> Get([ModelBinder(Name = "coord")]Point point)
        {
            return point;
        }


    }
}
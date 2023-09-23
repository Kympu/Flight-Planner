using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly FlightStorage _storage = new();

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirport(string search)
        {
            var flight = _storage.SearchAirport(search);
            var result = new[] { flight.from };

            return Ok(result);
        }
    }
}

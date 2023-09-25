using FlightPlanner.Models;
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
            var result = new[] { flight.From };

            return Ok(result);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult GetFlights(SearchFlightsRequest request)
        {
            var result = _storage.SearchFlight(request);

            if (request.From == request.To)
            {
                return BadRequest();
            }

            var pageResult = new PageResult<Flights>
            {
                Page = 0,
                TotalItems = result.Count,
                Items = result
            };

            return Ok(pageResult);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var result = _storage.FindFlightById(id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

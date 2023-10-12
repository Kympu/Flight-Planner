using FlightPlanner.Core.Models;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly FilterData _data;
        public CustomerApiController(FlightPlannerDbContext context)
        {
            _data = new FilterData(context);
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirport([FromQuery] string search)
        {
            var flight = _data.SearchAirport(search);
            return Ok(flight);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult GetFlights(SearchFlightsRequest request)
        {
            var result = _data.SearchFlight(request);

            if (request.From == request.To)
            {
                return BadRequest(request);
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
            var result = _data.FindFlightById(id);

            if(result == null)
            {
                return NotFound(id);
            }

            return Ok(result);
        }
    }
}

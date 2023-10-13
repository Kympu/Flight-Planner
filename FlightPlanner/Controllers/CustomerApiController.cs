using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Models;
using FlightPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {

        private readonly FilterData _data;
        private readonly AirportService _airportService;
        private readonly IMapper _mapper;
        public CustomerApiController(FlightPlannerDbContext context, IMapper mapper)
        {
            _data = new FilterData(context);
            _mapper = mapper;
            _airportService = new AirportService(context);
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirport(string search)
        {
            var airport = _airportService.GetAirport(search);

            var airportRequest = _mapper.Map<AirportRequest>(airport);

            return Ok(airportRequest);
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

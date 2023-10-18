using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Models;
using FlightPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {

        private readonly AirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        public CustomerApiController(
            FlightPlannerDbContext context, 
            IMapper mapper, 
            IFlightService flightService)
        {
            _mapper = mapper;
            _airportService = new AirportService(context);
            _flightService = flightService;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirport(string search)
        {
            var airports = _airportService.GetAirport(search);
            var airportRequest = airports.Select(a => _mapper.Map<AirportRequest>(a)).ToList();

            return Ok(airportRequest);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult GetFlights(SearchFlightsRequest request)
        {
            var result = _airportService.SearchFlight(request);


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
            var result = _flightService.GetFullFlightById(id);


            if (result == null)
            {
                return NotFound(id);
            }

            return Ok(_mapper.Map<FlightRequest>(result));
        }
    }
}

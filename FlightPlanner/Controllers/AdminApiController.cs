using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightStorage _storage;


        public AdminApiController()
        {
            _storage = new FlightStorage();
        }

        [Route("flights/{id}")]
        [HttpGet]
       public IActionResult GetFlight(int id)
        {
            return NotFound();
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flights flight)
        {
            if (_storage.CheckDuplicateEntry(flight) != null)
            {
                return Conflict();
            }

            if (!_storage.ValidateEntry(flight))
            {
                return BadRequest();
            }

            if (_storage.ValidateDestination(flight))
            {
                return BadRequest();
            }

            if (!_storage.ValidateDate(flight))
            {
                return BadRequest();
            }

            _storage.AddFlight(flight);

            return Created("", flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            _storage.DeleteFlight(id);

            return Ok();
        }
    }
}
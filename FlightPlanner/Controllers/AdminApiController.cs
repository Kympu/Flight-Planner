using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightStorage _storage;
        private static readonly object _locker = new object();

        public AdminApiController()
        {
            _storage = new FlightStorage();
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            return NotFound(id);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flights flight)
        {
            lock (_locker)
            {
                if (_storage.CheckDuplicateEntry(flight) != null)
                {
                    return Conflict(flight);
                }


                if (!_storage.ValidateEntry(flight)
                    || _storage.ValidateDestination(flight)
                    || !_storage.ValidateDate(flight))
                {
                    return BadRequest(flight);
                }

                _storage.AddFlight(flight);
            }
          
            return Created("", flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock(_locker)
            {
                _storage.DeleteFlight(id);
            }

            return Ok(id);
        }
    }
}
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FilterData _data;
        private readonly FlightPlannerDbContext _context;
        private static readonly object _locker = new object();

        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
            _data = new FilterData(context);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flights flight)
        {
            lock (_locker)
            {
                if (_data.CheckDuplicateEntry(flight) != null)
                {
                    return Conflict(flight);
                }

                if (!_data.ValidateEntry(flight)
                     || _data.ValidateDestination(flight)
                     || !_data.ValidateDate(flight)) 
                 {
                     return BadRequest(flight);
                 }
 
                _context.Flights.Add(flight);
                _context.SaveChanges();
            }

            return Created("", flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock(_locker)
            {
                var flightToDelete = _context.Flights.Find(id);

                if (flightToDelete != null)
                {
                    _context.Flights.Remove(flightToDelete);
                    _context.SaveChanges();
                }
            }
            return Ok(id);
        }
    }
}
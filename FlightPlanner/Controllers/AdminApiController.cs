﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        [Route("flights/{id}")]
        [HttpGet]
       public IActionResult GetFlight(int id)
        {
            return NotFound();
        }
    }
}
 
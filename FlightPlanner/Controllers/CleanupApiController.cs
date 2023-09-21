using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupApiController : ControllerBase
    {
        [Route("clear")]
        [HttpPost]
        public IActionResult GetActionClear()
        {
            return Ok();
        }
    }
}

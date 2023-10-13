using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        Airport? GetAirport(Airport airport);
        bool Exists(Airport airport);
    }
}

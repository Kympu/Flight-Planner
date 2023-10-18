using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        List<Airport> GetAirport(string search);
        List<Flights> SearchFlight(SearchFlightsRequest request);
    }
}

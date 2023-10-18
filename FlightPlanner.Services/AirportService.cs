using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Models;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public List<Airport> GetAirport(string search)
        {
            search = search.ToLower().Trim();

            var airports = _context.Airports
                .AsEnumerable()
                .Where(a => new[] { a.Country, a.City, a.AirportCode }
                    .Any(attr => attr.ToLower().Contains(search)))
                .ToList();

            return airports;
        }

        public List<Flights> SearchFlight(SearchFlightsRequest request)
        {
            var flight = _context.Flights
                .Where(f =>
                    f.From.AirportCode.Contains(request.From)
                    && f.To.AirportCode.Contains(request.To)
                    && f.DepartureTime.Contains(request.DepartureDate))
                .ToList();

            return flight;
        }
    }
}

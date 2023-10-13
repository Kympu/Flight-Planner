using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public bool Exists(Airport airport)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Airport> GetAirport(string search)
        {
            var airports = _context.Airports
                .Where(a => a.AirportCode.ToUpper().Contains(search)
                    || a.City.ToUpper().Contains(search)
                    || a.Country.ToUpper().Contains(search));

            return airports;
        }

        public Airport? GetAirport(Airport airport)
        {
            throw new NotImplementedException();
        }
    }
}

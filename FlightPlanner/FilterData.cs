using FlightPlanner.Core.Models;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner
{
    public class FilterData
    {

        private readonly FlightPlannerDbContext _context;

        public FilterData(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public Flights? CheckDuplicateEntry(Flights flight)
        {
            var isDuplicate = _context.Flights.Any(existingFlight =>
                existingFlight.From.AirportCode == flight.From.AirportCode &&
                existingFlight.From.Country == flight.From.Country &&
                existingFlight.From.City == flight.From.City &&
                existingFlight.To.AirportCode == flight.To.AirportCode &&
                existingFlight.To.Country == flight.To.Country &&
                existingFlight.To.City == flight.To.City &&
                existingFlight.Carrier == flight.Carrier &&
                existingFlight.DepartureTime == flight.DepartureTime &&
                existingFlight.ArrivalTime == flight.ArrivalTime);

            if (isDuplicate)
            {
                return flight;
            }

            return null;
        }

        public bool ValidateEntry(Flights flight)
        {
            return new[]
            {
                flight.From.Country,
                flight.From.City,
                flight.From.AirportCode,
                flight.To.Country,
                flight.To.City,
                flight.To.AirportCode,
                flight.Carrier,
                flight.DepartureTime,
                flight.ArrivalTime
            }
            .All(s => !string.IsNullOrEmpty(s));
        }

        public bool ValidateDestination(Flights flight)
        {
            return (flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower());
        }

        public bool ValidateDate(Flights flight)
        {
            DateTime departureTime;
            DateTime arrivalTime;

            if (!DateTime.TryParse(flight.DepartureTime, out departureTime) ||
                !DateTime.TryParse(flight.ArrivalTime, out arrivalTime))
            {
                return false;
            }

            if (departureTime >= arrivalTime)
            {
                return false;
            }

            return true;
        }

        public List<Airport> SearchAirport(string search)
        {
            search = search.ToLower().Trim();

            var airports = _context.Airports
                .Where(a => a.AirportCode.ToUpper().Contains(search)
                    || a.City.ToUpper().Contains(search)
                    || a.Country.ToUpper().Contains(search))
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

        public Flights FindFlightById(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(s => s.Id == id);

            return flight;
        }
    }
}

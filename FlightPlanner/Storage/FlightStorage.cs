using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        private static List<Flights> _flightStorage = new List<Flights>();
        private static int _id = 0;

        public void AddFlight(Flights flight)
        {
            flight.Id = _id++;
            _flightStorage.Add(flight);
        }

        public void Clear()
        {
            _flightStorage.Clear();
        }

        public List<Flights> GetFlights()
        {
            return _flightStorage;
        }

        public Flights? CheckDuplicateEntry(Flights flight)
        {
            return _flightStorage.Where(existingFlight =>
                existingFlight.From.AirportCode == flight.From.AirportCode &&
                existingFlight.From.Country == flight.From.Country &&
                existingFlight.From.City == flight.From.City &&
                existingFlight.To.AirportCode == flight.To.AirportCode &&
                existingFlight.To.Country == flight.To.Country &&
                existingFlight.To.City == flight.To.City &&
                existingFlight.Carrier == flight.Carrier &&
                existingFlight.DepartureTime == flight.DepartureTime &&
                existingFlight.ArrivalTime == flight.ArrivalTime)
                .ToList().FirstOrDefault();
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

        public void DeleteFlight(int id)
        {
            _flightStorage.RemoveAll(flight => flight.Id == id);
        }

        public Flights SearchAirport(string search)
        {
            var flight = _flightStorage
                .FirstOrDefault(s => new[] { s.From.Country, s.From.City, s.From.AirportCode }
                .Any(attr => attr.ToLower().Contains(search.ToLower().Trim())));

            return flight;
        }

        public List<Flights> SearchFlight(SearchFlightsRequest request)
        {
            var flight = _flightStorage
                .Where(f =>
                    f.From.AirportCode.Contains(request.From)
                    && f.To.AirportCode.Contains(request.To)
                    && f.DepartureTime.Contains(request.DepartureDate))
                .ToList();

            return flight;
        }

        public Flights FindFlightById(int id)
        {
            return _flightStorage.FirstOrDefault(s => s.Id == id);
        }
    }
}

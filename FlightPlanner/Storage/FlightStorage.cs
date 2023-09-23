using FlightPlanner.Models;
using System.Reflection;

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
                existingFlight.from.AirportCode == flight.from.AirportCode &&
                existingFlight.from.Country == flight.from.Country &&
                existingFlight.from.City == flight.from.City &&
                existingFlight.to.AirportCode == flight.to.AirportCode &&
                existingFlight.to.Country == flight.to.Country &&
                existingFlight.to.City == flight.to.City &&
                existingFlight.Carrier == flight.Carrier &&
                existingFlight.DepartureTime == flight.DepartureTime &&
                existingFlight.ArrivalTime == flight.ArrivalTime)
                .ToList().FirstOrDefault();
        }

        public bool ValidateEntry(Flights flight)
        {
            return new[]
            {
                flight.from.Country,
                flight.from.City,
                flight.from.AirportCode,
                flight.to.Country,
                flight.to.City,
                flight.to.AirportCode,
                flight.Carrier,
                flight.DepartureTime,
                flight.ArrivalTime
            }
            .All(s => !string.IsNullOrEmpty(s));
        }

        public bool ValidateDestination(Flights flight)
        {
            return (flight.from.AirportCode.Trim().ToLower() == flight.to.AirportCode.Trim().ToLower());
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

            if (departureTime.Date >= arrivalTime.Date)
            {
                return false;
            }

            return true;
        }
    }
}

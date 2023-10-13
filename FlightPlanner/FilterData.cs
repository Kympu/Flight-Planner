using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner
{
    public class FilterData
    {

        private readonly FlightPlannerDbContext _context;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public FilterData(FlightPlannerDbContext context)
        {
            _context = context;
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

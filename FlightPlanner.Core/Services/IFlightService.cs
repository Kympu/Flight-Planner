using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flights>
    {
        Flights? GetFullFlightById(int id);

        bool Exists(Flights flight);
    }
}

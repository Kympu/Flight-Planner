using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Validations
{
    public class FlightValueValidator : IValidate
    {
        public bool IsValid(Flights flight)
        {
            return
                !string.IsNullOrEmpty(flight?.ArrivalTime) &&
                !string.IsNullOrEmpty(flight?.DepartureTime) &&
                !string.IsNullOrEmpty(flight?.Carrier) &&
                flight?.To != null &&
                flight?.From != null;
        }
    }
}

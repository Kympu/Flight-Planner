namespace FlightPlanner.Models
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }

        public SearchFlightsRequest(string from, string to, string departureDate)
        {
            From = from;
            To = to;
            DepartureDate = departureDate;
        }
    }
}

namespace FlightPlanner.Models
{

     public class Flights
    {
        public int Id { get; set; }
        public Airport from { get; set; }
        public Airport to { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
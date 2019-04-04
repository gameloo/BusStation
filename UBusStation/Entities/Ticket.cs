namespace UBusStation.Entities.Profile
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Seat { get; set; }
        public string DateBuy { get; set; }
        public double Coast { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public int? TripId { get; set; }
        public Trip Trip { get; set; }
    }
}

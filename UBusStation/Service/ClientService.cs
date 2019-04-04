using UBusStation.Context;
using UBusStation.Entities.Profile;

namespace UBusStation.Service
{
    public class ClientService
    {
        public static void BuyTicket(Ticket ticket)
        {
            using (BusStationDatabase db = new BusStationDatabase())
            {
                db.Ticket.Add(ticket);
                db.SaveChanges();
            }
        }
    }
}

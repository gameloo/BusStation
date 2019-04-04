using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UBusStation.Context;
using UBusStation.Entities.Profile;

namespace UBusStation.Service
{
    public class AutorizationService
    {

        public static bool Registration(Client client)
        {
            using (BusStationDatabase db = new BusStationDatabase())
            {
                db.Client.Add(client);
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }

        public static Client LoginClient(string username, string password)
        {
            using (BusStationDatabase db = new BusStationDatabase())
            {
                try
                {
                    return db.Client.First(u => ((u.Mail == username || u.PhoneNumber == username) && u.Password == password));
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
        }

        public static Employee LoginEmployee(string username, string password)
        {
            using (BusStationDatabase db = new BusStationDatabase())
            {
                try
                {
                    return db.Employee.First(u => (u.Username == username && u.Password == password));
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
        }
    }
}

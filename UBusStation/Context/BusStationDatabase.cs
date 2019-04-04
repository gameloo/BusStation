using Microsoft.EntityFrameworkCore;
using UBusStation.Entities;
using UBusStation.Entities.Profile;

namespace UBusStation.Context
{
    public class BusStationDatabase: DbContext
    {
        public DbSet<Bus> Bus { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Position> Position { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=newDBuStation.db");
        }
    }
}

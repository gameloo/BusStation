using System.Collections.Generic;

namespace UBusStation.Entities
{
    public class Bus
    {
        public int Id { get; set; }
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string LicensePlate { get; set; }

        public virtual ICollection<Trip> Trip { get; set; }

        public Bus()
        {
            Trip = new List<Trip>();
        }
    }
}

using System.Collections.Generic;
using UBusStation.Entities.Profile;

namespace UBusStation.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        
        public string TripNumber { get; set; }

        /// <summary>
        /// Время отбытия
        /// </summary>
        public string DateDep { get; set; }
        /// <summary>
        /// Время прибытия в пункт назначения
        /// </summary>
        public string DateArr { get; set; }

        /// <summary>
        /// ID автобуса
        /// </summary>
        public int? BusId { get; set; }
        public Bus Bus { get; set; }
        /// <summary>
        /// ID водителя автобуса
        /// </summary>
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        /// <summary>
        /// Номер маршрута
        /// </summary>

        public virtual ICollection<Ticket> Ticket { get; set; }

        public Trip()
        {
            Ticket = new List<Ticket>();
        }
    }
}
